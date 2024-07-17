using CoreBusiness;
using System.Text.Json;
using System.Text;
using WebApp.Factory.Interfaces;

namespace WebApp.Factory
{
    public class SalesClient : ISalesClient
    {
        private readonly HttpClient _httpClient;
        public SalesClient(HttpClient clientFactory)
        {
            _httpClient = clientFactory;

        }


        public async Task<IEnumerable<Product>> GetProductById(int categoryId)
        {
            string path = $"/{categoryId}";
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + path);
            string data = await response.Content.ReadAsStringAsync();
            var value = JsonSerializer.Deserialize<IEnumerable<Product>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return value;
        }
        public async Task AddSalesViewModel(ViewModels.SalesViewModel prod)
        {
            string path = "";
            var input = JsonSerializer.Serialize(prod);
            HttpResponseMessage response = await _httpClient.PostAsync(path, new StringContent(input, Encoding.Default, "application/json"));
            response.EnsureSuccessStatusCode();
        }


    }
}
