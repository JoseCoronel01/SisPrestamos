using ClosedXML.Excel;
using SisPrestamos.Model;

namespace SisPrestamos.Service
{
    public class ExcelExportService
    {
        public byte[] ExportClientesToExcel(List<Cliente> clientes)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Clientes");

                // Headers
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nombre";
                worksheet.Cell(1, 3).Value = "Teléfono";
                worksheet.Cell(1, 4).Value = "Email";
                worksheet.Cell(1, 5).Value = "Dirección";
                worksheet.Cell(1, 6).Value = "Ciudad";
                worksheet.Cell(1, 7).Value = "Estado";
                worksheet.Cell(1, 8).Value = "País";

                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 8);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;

                // Data
                for (int i = 0; i < clientes.Count; i++)
                {
                    var cliente = clientes[i];
                    worksheet.Cell(i + 2, 1).Value = cliente.Id;
                    worksheet.Cell(i + 2, 2).Value = cliente.Name;
                    worksheet.Cell(i + 2, 3).Value = cliente.Phone;
                    worksheet.Cell(i + 2, 4).Value = cliente.Email;
                    worksheet.Cell(i + 2, 5).Value = cliente.Address;
                    worksheet.Cell(i + 2, 6).Value = cliente.City;
                    worksheet.Cell(i + 2, 7).Value = cliente.State;
                    worksheet.Cell(i + 2, 8).Value = cliente.Country;
                }

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public byte[] ExportLineasToExcel(List<Linea> lineas, List<Cliente> clientes)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Líneas de Crédito");

                // Headers
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Cliente";
                worksheet.Cell(1, 3).Value = "Monto";
                worksheet.Cell(1, 4).Value = "Fecha";
                worksheet.Cell(1, 5).Value = "Hora";

                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 5);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGreen;

                // Data
                for (int i = 0; i < lineas.Count; i++)
                {
                    var linea = lineas[i];
                    var cliente = clientes.FirstOrDefault(c => c.Id == linea.ClienteId);
                    
                    worksheet.Cell(i + 2, 1).Value = linea.Id;
                    worksheet.Cell(i + 2, 2).Value = cliente?.Name ?? "N/A";
                    worksheet.Cell(i + 2, 3).Value = linea.Monto;
                    worksheet.Cell(i + 2, 3).Style.NumberFormat.Format = "$#,##0.00";
                    worksheet.Cell(i + 2, 4).Value = linea.Fecha.ToString("yyyy-MM-dd");
                    worksheet.Cell(i + 2, 5).Value = linea.Hora.ToString(@"hh\:mm\:ss");
                }

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public byte[] ExportCreditosToExcel(List<Credito> creditos, List<Linea> lineas, List<Cliente> clientes)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Créditos");

                // Headers
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Cliente";
                worksheet.Cell(1, 3).Value = "Línea ID";
                worksheet.Cell(1, 4).Value = "Importe";
                worksheet.Cell(1, 5).Value = "Fecha";
                worksheet.Cell(1, 6).Value = "Hora";

                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 6);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightYellow;

                // Data
                for (int i = 0; i < creditos.Count; i++)
                {
                    var credito = creditos[i];
                    var linea = lineas.FirstOrDefault(l => l.Id == credito.LineaId);
                    var cliente = clientes.FirstOrDefault(c => c.Id == linea?.ClienteId);
                    
                    worksheet.Cell(i + 2, 1).Value = credito.Id;
                    worksheet.Cell(i + 2, 2).Value = cliente?.Name ?? "N/A";
                    worksheet.Cell(i + 2, 3).Value = credito.LineaId;
                    worksheet.Cell(i + 2, 4).Value = credito.Importe;
                    worksheet.Cell(i + 2, 4).Style.NumberFormat.Format = "$#,##0.00";
                    worksheet.Cell(i + 2, 5).Value = credito.Fecha.ToString("yyyy-MM-dd");
                    worksheet.Cell(i + 2, 6).Value = credito.Hora.ToString(@"hh\:mm\:ss");
                }

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public byte[] ExportPagosToExcel(List<Pago> pagos, List<Credito> creditos, List<Linea> lineas, List<Cliente> clientes)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Pagos");

                // Headers
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Cliente";
                worksheet.Cell(1, 3).Value = "Crédito ID";
                worksheet.Cell(1, 4).Value = "Importe Pagado";
                worksheet.Cell(1, 5).Value = "Fecha";
                worksheet.Cell(1, 6).Value = "Hora";

                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 6);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightCyan;

                // Data
                for (int i = 0; i < pagos.Count; i++)
                {
                    var pago = pagos[i];
                    var credito = creditos.FirstOrDefault(c => c.Id == pago.CreditoId);
                    var linea = lineas.FirstOrDefault(l => l.Id == credito?.LineaId);
                    var cliente = clientes.FirstOrDefault(c => c.Id == linea?.ClienteId);
                    
                    worksheet.Cell(i + 2, 1).Value = pago.Id;
                    worksheet.Cell(i + 2, 2).Value = cliente?.Name ?? "N/A";
                    worksheet.Cell(i + 2, 3).Value = pago.CreditoId;
                    worksheet.Cell(i + 2, 4).Value = pago.Importe;
                    worksheet.Cell(i + 2, 4).Style.NumberFormat.Format = "$#,##0.00";
                    worksheet.Cell(i + 2, 5).Value = pago.Fecha.ToString("yyyy-MM-dd");
                    worksheet.Cell(i + 2, 6).Value = pago.Hora.ToString(@"hh\:mm\:ss");
                }

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
