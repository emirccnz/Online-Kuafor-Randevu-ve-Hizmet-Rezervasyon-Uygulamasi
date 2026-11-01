using Microsoft.JSInterop;
using System.Text.Json;
namespace Online_Kuaför_Randevu_ve_Hizmet_Rezervasyon_Uygulaması.Client.Services
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetItemAsync<T>(string key, T item)
        {
            var json = JsonSerializer.Serialize(item);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            return String.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json);
        }
    }
}
