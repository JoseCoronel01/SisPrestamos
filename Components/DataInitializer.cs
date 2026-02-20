using SisPrestamos.Model;
using SisPrestamos.Service;

namespace SisPrestamos.Components
{
    public class DataInitializer
    {
        public static async Task InitializeDefaultDataAsync(
            ClienteService clienteService,
            LineaService lineaService,
            CreditoService creditoService,
            PagoService pagoService)
        {
            var clientes = await clienteService.GetAllAsync();
            
            if (clientes.Count == 0)
            {
                // Datos de ejemplo
                var clientesDefault = new List<Cliente>
                {
                    new() { Name = "Ana", Phone = "1234567890", Email = "ana@live.com", Address = "Callex", City = "Mochis", State = "Sinaloa", Country = "Mexico" },
                    new() { Name = "Luis", Phone = "1234567890", Email = "luis@live.com", Address = "Callex", City = "Mochis", State = "Sinaloa", Country = "Mexico" },
                    new() { Name = "Pedro", Phone = "1234567890", Email = "pedro@live.com", Address = "Callex", City = "Mochis", State = "Sinaloa", Country = "Mexico" }
                };

                foreach (var cliente in clientesDefault)
                {
                    await clienteService.AddAsync(cliente);
                }

                // Líneas de crédito de ejemplo
                var lineasDefault = new List<Linea>
                {
                    new() { ClienteId = 1, Monto = 50000 },
                    new() { ClienteId = 2, Monto = 30000 },
                    new() { ClienteId = 3, Monto = 75000 }
                };

                foreach (var linea in lineasDefault)
                {
                    await lineaService.AddAsync(linea);
                }

                // Créditos de ejemplo
                var creditosDefault = new List<Credito>
                {
                    new() { LineaId = 1, Importe = 25000 },
                    new() { LineaId = 2, Importe = 15000 },
                    new() { LineaId = 3, Importe = 40000 }
                };

                foreach (var credito in creditosDefault)
                {
                    await creditoService.AddAsync(credito);
                }

                // Pagos de ejemplo
                var pagosDefault = new List<Pago>
                {
                    new() { CreditoId = 1, Importe = 5000 },
                    new() { CreditoId = 2, Importe = 3000 },
                    new() { CreditoId = 3, Importe = 8000 }
                };

                foreach (var pago in pagosDefault)
                {
                    await pagoService.AddAsync(pago);
                }
            }
        }
    }
}
