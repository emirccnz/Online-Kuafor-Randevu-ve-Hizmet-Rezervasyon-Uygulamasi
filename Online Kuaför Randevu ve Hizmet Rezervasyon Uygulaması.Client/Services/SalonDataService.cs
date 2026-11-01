using System.Net.Http.Json;
using Data.Models;

namespace Online_Kuaför_Randevu_ve_Hizmet_Rezervasyon_Uygulaması.Client.Services
{
    public class SalonDataService
    {
        private readonly HttpClient _httpClient;
        public SalonDataService(HttpClient httpClient) => _httpClient = httpClient;

        private async Task<List<T>> LoadDataAsync<T>(string fileName)
        {
            try
            {
                var data = await _httpClient.GetFromJsonAsync<List<T>>($"Datas/{fileName}");
                return data ?? new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        public async Task<List<Service>> GetServicesAsync() => await LoadDataAsync<Service>("services.json");
        public async Task<List<Employee>> GetEmployeesAsync() => await LoadDataAsync<Employee>("employees.json");
        public async Task<List<BusySlot>> GetBusySlotsAsync() => await LoadDataAsync<BusySlot>("busy-slots.json");
    }
}