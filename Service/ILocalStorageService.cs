using System.Text.Json;
using SisPrestamos.Model;

namespace SisPrestamos.Service
{
    public interface ILocalStorageService
    {
        Task<string> GetItemAsync(string key);
        Task SetItemAsync(string key, string value);
        Task RemoveItemAsync(string key);
        Task ClearAsync();
    }
}
