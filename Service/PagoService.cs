using System.Text.Json;
using SisPrestamos.Model;

namespace SisPrestamos.Service
{
    public class PagoService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "pagos";

        public PagoService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<Pago>> GetAllAsync()
        {
            var json = await _localStorage.GetItemAsync(StorageKey);
            return string.IsNullOrEmpty(json) 
                ? new List<Pago>() 
                : JsonSerializer.Deserialize<List<Pago>>(json) ?? new List<Pago>();
        }

        public async Task<List<Pago>> GetByCreditoIdAsync(int creditoId)
        {
            var pagos = await GetAllAsync();
            return pagos.Where(p => p.CreditoId == creditoId).ToList();
        }

        public async Task<Pago> GetByIdAsync(int id)
        {
            var pagos = await GetAllAsync();
            return pagos.FirstOrDefault(p => p.Id == id);
        }

        public async Task AddAsync(Pago pago)
        {
            var pagos = await GetAllAsync();
            pago.Id = pagos.Count > 0 ? pagos.Max(p => p.Id) + 1 : 1;
            pago.Fecha = DateTime.Now;
            pago.Hora = DateTime.Now.TimeOfDay;
            pagos.Add(pago);
            await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(pagos));
        }

        public async Task UpdateAsync(Pago pago)
        {
            var pagos = await GetAllAsync();
            var index = pagos.FindIndex(p => p.Id == pago.Id);
            if (index >= 0)
            {
                pagos[index] = pago;
                await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(pagos));
            }
        }

        public async Task DeleteAsync(int id)
        {
            var pagos = await GetAllAsync();
            pagos.RemoveAll(p => p.Id == id);
            await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(pagos));
        }
    }
}
