using ClosedXML.Excel;
using SisPrestamos.Model;

namespace SisPrestamos.Service
{
    public class ExcelImportService
    {
        public async Task<(List<Cliente> clientes, string mensaje)> ImportClientesFromExcelAsync(Stream fileStream)
        {
            var clientes = new List<Cliente>();
            var mensaje = "";

            try
            {
                // Convertir el stream asincrónico a MemoryStream sincrónico
                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var workbook = new XLWorkbook(memoryStream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            try
                            {
                                var cliente = new Cliente
                                {
                                    Name = row.Cell(2).Value.ToString() ?? "",
                                    Phone = row.Cell(3).Value.ToString() ?? "",
                                    Email = row.Cell(4).Value.ToString() ?? "",
                                    Address = row.Cell(5).Value.ToString() ?? "",
                                    City = row.Cell(6).Value.ToString() ?? "",
                                    State = row.Cell(7).Value.ToString() ?? "",
                                    Country = row.Cell(8).Value.ToString() ?? ""
                                };

                                if (!string.IsNullOrWhiteSpace(cliente.Name))
                                {
                                    clientes.Add(cliente);
                                }
                            }
                            catch
                            {
                                // Ignorar filas con errores
                            }
                        }

                        mensaje = $"Se importaron exitosamente {clientes.Count} clientes.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al importar el archivo: {ex.Message}";
            }

            return (clientes, mensaje);
        }

        public async Task<(List<Linea> lineas, string mensaje)> ImportLineasFromExcelAsync(Stream fileStream, List<Cliente> clientes)
        {
            var lineas = new List<Linea>();
            var mensaje = "";

            try
            {
                // Convertir el stream asincrónico a MemoryStream sincrónico
                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var workbook = new XLWorkbook(memoryStream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            try
                            {
                                var clienteNombre = row.Cell(2).Value.ToString() ?? "";
                                var cliente = clientes.FirstOrDefault(c => c.Name.Equals(clienteNombre, StringComparison.OrdinalIgnoreCase));

                                if (cliente == null)
                                {
                                    continue;
                                }

                                if (decimal.TryParse(row.Cell(3).Value.ToString(), out var monto) &&
                                    DateTime.TryParse(row.Cell(4).Value.ToString(), out var fecha))
                                {
                                    var linea = new Linea
                                    {
                                        ClienteId = cliente.Id,
                                        Monto = monto,
                                        Fecha = fecha,
                                        Hora = TimeSpan.TryParse(row.Cell(5).Value.ToString(), out var hora) ? hora : TimeSpan.Zero
                                    };

                                    lineas.Add(linea);
                                }
                            }
                            catch
                            {
                                // Ignorar filas con errores
                            }
                        }

                        mensaje = $"Se importaron exitosamente {lineas.Count} líneas de crédito.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al importar el archivo: {ex.Message}";
            }

            return (lineas, mensaje);
        }

        public async Task<(List<Credito> creditos, string mensaje)> ImportCreditosFromExcelAsync(Stream fileStream, List<Linea> lineas)
        {
            var creditos = new List<Credito>();
            var mensaje = "";

            try
            {
                // Convertir el stream asincrónico a MemoryStream sincrónico
                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var workbook = new XLWorkbook(memoryStream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            try
                            {
                                if (int.TryParse(row.Cell(3).Value.ToString(), out var lineaId) &&
                                    decimal.TryParse(row.Cell(4).Value.ToString(), out var importe) &&
                                    DateTime.TryParse(row.Cell(5).Value.ToString(), out var fecha))
                                {
                                    var linea = lineas.FirstOrDefault(l => l.Id == lineaId);
                                    if (linea == null)
                                    {
                                        continue;
                                    }

                                    var credito = new Credito
                                    {
                                        LineaId = lineaId,
                                        Importe = importe,
                                        Fecha = fecha,
                                        Hora = TimeSpan.TryParse(row.Cell(6).Value.ToString(), out var hora) ? hora : TimeSpan.Zero
                                    };

                                    creditos.Add(credito);
                                }
                            }
                            catch
                            {
                                // Ignorar filas con errores
                            }
                        }

                        mensaje = $"Se importaron exitosamente {creditos.Count} créditos.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al importar el archivo: {ex.Message}";
            }

            return (creditos, mensaje);
        }

        public async Task<(List<Pago> pagos, string mensaje)> ImportPagosFromExcelAsync(Stream fileStream, List<Credito> creditos)
        {
            var pagos = new List<Pago>();
            var mensaje = "";

            try
            {
                // Convertir el stream asincrónico a MemoryStream sincrónico
                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var workbook = new XLWorkbook(memoryStream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            try
                            {
                                if (int.TryParse(row.Cell(3).Value.ToString(), out var creditoId) &&
                                    decimal.TryParse(row.Cell(4).Value.ToString(), out var importe) &&
                                    DateTime.TryParse(row.Cell(5).Value.ToString(), out var fecha))
                                {
                                    var credito = creditos.FirstOrDefault(c => c.Id == creditoId);
                                    if (credito == null)
                                    {
                                        continue;
                                    }

                                    var pago = new Pago
                                    {
                                        CreditoId = creditoId,
                                        Importe = importe,
                                        Fecha = fecha,
                                        Hora = TimeSpan.TryParse(row.Cell(6).Value.ToString(), out var hora) ? hora : TimeSpan.Zero
                                    };

                                    pagos.Add(pago);
                                }
                            }
                            catch
                            {
                                // Ignorar filas con errores
                            }
                        }

                        mensaje = $"Se importaron exitosamente {pagos.Count} pagos.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al importar el archivo: {ex.Message}";
            }

            return (pagos, mensaje);
        }
    }
}
