using System.Net.Http.Json;
using Data.Models;

namespace Online_Kuaför_Randevu_ve_Hizmet_Rezervasyon_Uygulaması.Client.Services
{
    public class SalonDataService
    {
        private readonly HttpClient _httpClient;
        
        public SalonDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<List<T>> LoadDataAsync<T>(string fileName)
        {
            return await _httpClient.GetFromJsonAsync<List<T>>($"Datas/{fileName}");
        }

        public async Task<List<Service>> GetServicesAsync()
        {
            return await LoadDataAsync<Service>("services.json");
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await LoadDataAsync<Employee>("employees.json");
        }

        public async Task<List<BusySlot>> GetBusySlotsAsync()
        {
            return await LoadDataAsync<BusySlot>("busy-slots.json");
        }
    }
}
