using System.Text.Json;
using SisPrestamos.Model;

namespace SisPrestamos.Service
{
    public class CreditoService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "creditos";

        public CreditoService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<Credito>> GetAllAsync()
        {
            var json = await _localStorage.GetItemAsync(StorageKey);
            return string.IsNullOrEmpty(json) 
                ? new List<Credito>() 
                : JsonSerializer.Deserialize<List<Credito>>(json) ?? new List<Credito>();
        }

        public async Task<List<Credito>> GetByLineaIdAsync(int lineaId)
        {
            var creditos = await GetAllAsync();
            return creditos.Where(c => c.LineaId == lineaId).ToList();
        }

        public async Task<Credito> GetByIdAsync(int id)
        {
            var creditos = await GetAllAsync();
            return creditos.FirstOrDefault(c => c.Id == id);
        }

        public async Task AddAsync(Credito credito)
        {
            var creditos = await GetAllAsync();
            credito.Id = creditos.Count > 0 ? creditos.Max(c => c.Id) + 1 : 1;
            credito.Fecha = DateTime.Now;
            credito.Hora = DateTime.Now.TimeOfDay;
            creditos.Add(credito);
            await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(creditos));
        }

        public async Task UpdateAsync(Credito credito)
        {
            var creditos = await GetAllAsync();
            var index = creditos.FindIndex(c => c.Id == credito.Id);
            if (index >= 0)
            {
                creditos[index] = credito;
                await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(creditos));
            }
        }

        public async Task DeleteAsync(int id)
        {
            var creditos = await GetAllAsync();
            creditos.RemoveAll(c => c.Id == id);
            await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(creditos));
        }
    }
}
