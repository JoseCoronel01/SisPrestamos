using System.Text.Json;
using SisPrestamos.Model;

namespace SisPrestamos.Service
{
    public class ClienteService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "clientes";

        public ClienteService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            var json = await _localStorage.GetItemAsync(StorageKey);
            return string.IsNullOrEmpty(json) 
                ? new List<Cliente>() 
                : JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            var clientes = await GetAllAsync();
            return clientes.FirstOrDefault(c => c.Id == id);
        }

        public async Task AddAsync(Cliente cliente)
        {
            var clientes = await GetAllAsync();
            cliente.Id = clientes.Count > 0 ? clientes.Max(c => c.Id) + 1 : 1;
            clientes.Add(cliente);
            await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(clientes));
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            var clientes = await GetAllAsync();
            var index = clientes.FindIndex(c => c.Id == cliente.Id);
            if (index >= 0)
            {
                clientes[index] = cliente;
                await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(clientes));
            }
        }

        public async Task DeleteAsync(int id)
        {
            var clientes = await GetAllAsync();
            clientes.RemoveAll(c => c.Id == id);
            await _localStorage.SetItemAsync(StorageKey, JsonSerializer.Serialize(clientes));
        }
    }
}
