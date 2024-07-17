using CoreBusiness;
using System.Text.Json;
using System.Text;
using WebApp.Factory.Interfaces;
using WebApp.ViewModels;
using IdentityModel.Client;

namespace WebApp.Factory
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenClient _tokenClient;

        public ProductClient(HttpClient clientFactory, ITokenClient tokenClient)
        {
            _httpClient = clientFactory;
            _tokenClient = tokenClient;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            Task.Run(async () =>
            {
                var tokenResponse = await _tokenClient.getToken();
                _httpClient.SetBearerToken(tokenResponse.AccessToken!);
            }).Wait();
            string path = "";
            HttpResponseMessage response = await _httpClient.GetAsync(path);
            string data = await response.Content.ReadAsStringAsync();
            var values = JsonSerializer.Deserialize<IEnumerable<Product>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return values;
        }

        public async Task<Product> GetProduct(int id)
        {
            string path = $"/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress +  path);
            string data = await response.Content.ReadAsStringAsync();
            var value = JsonSerializer.Deserialize<Product>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return value;
        }
        public async Task AddProduct(Product prod)
        {
            string path = "";
            var input = JsonSerializer.Serialize(prod);
            HttpResponseMessage response = await _httpClient.PostAsync(path, new StringContent(input, Encoding.Default, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task EditProduct(Product prod)
        {
            string path = "";
            var input = JsonSerializer.Serialize(prod);
            HttpResponseMessage response = await _httpClient.PatchAsync(path, new StringContent(input, Encoding.Default, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProduct(int id)
        {
            string path = $"?id={id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + path);
            response.EnsureSuccessStatusCode();
        }
    }
}
