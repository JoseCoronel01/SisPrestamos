using System.Text.Json;
using SisPrestamos.Model;

namespace SisPrestamos.Service
{
    public class LineaService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "lineas";

        public LineaService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<Linea>> GetAllAsync()
        {
            var json = await _localStorage.GetItemAsync(StorageKey);
            return string.IsNullOrEmpty(json) 
                ? new List<Linea>() 
                : JsonSerializer.Deserialize<List<Linea>>(json) ?? new List<Linea>();
        }

        public async Task<List<Linea>> GetByClienteIdAsync(int clienteId)
        {
            var lineas = await GetAllAsync();
            return lineas.Where(l => l.ClienteId == clienteId).ToList();
        }

        public async Task<Linea> GetByIdAsync(int id)
        {
            var lineas = await GetAllAsync();
            return lineas.FirstOrDefault(l => l.Id == id);
        }

        public async Task AddAsync(Linea linea)
        {
            var lineas = await GetAllAsync();
            linea.Id = lineas.Count > 0 ? lineas.Max(l => l.Id) + 1 : 1;
            linea.Fecha = DateTime.Now;
            linea.Hora = DateTime.Now.TimeOfDay;
            lineas.Add(linea);
            await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(lineas));
        }

        public async Task UpdateAsync(Linea linea)
        {
            var lineas = await GetAllAsync();
            var index = lineas.FindIndex(l => l.Id == linea.Id);
            if (index >= 0)
            {
                lineas[index] = linea;
                await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(lineas));
            }
        }

        public async Task DeleteAsync(int id)
        {
            var lineas = await GetAllAsync();
            lineas.RemoveAll(l => l.Id == id);
            await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(lineas));
        }
    }
}
