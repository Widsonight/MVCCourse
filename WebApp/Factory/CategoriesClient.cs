using CoreBusiness;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using WebApp.Factory.Interfaces;

namespace WebApp.Factory
{
    public class CategoriesClient : ICategoriesClient
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenClient _tokenClient;
        public CategoriesClient(HttpClient clientFactory, ITokenClient tokenClient)
        {
            _httpClient = clientFactory;
            _tokenClient = tokenClient;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {

            Task.Run(async () =>
            {
                var tokenResponse = await _tokenClient.getToken();
                _httpClient.SetBearerToken(tokenResponse.AccessToken!);
            }).Wait();
            string path = "";
            HttpResponseMessage response =  await _httpClient.GetAsync(path);
            string data= await response.Content.ReadAsStringAsync();
            var values = JsonSerializer.Deserialize<IEnumerable<Category>>(data,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return values;
        }

        public async Task<Category> GetCategorie(int id)
        {
            string path = $"/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + path);
            string data = await response.Content.ReadAsStringAsync();
            var value = JsonSerializer.Deserialize<Category>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return value;
        }
        public async Task AddCategorie(Category category)
        {
            string path = "";
            var input= JsonSerializer.Serialize(category);
            HttpResponseMessage response = await _httpClient.PostAsync(path, new StringContent(input, Encoding.Default, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task EditCategorie(Category category)
        {
            string path = "";
            var input = JsonSerializer.Serialize(category);
            HttpResponseMessage response = await _httpClient.PatchAsync(path, new StringContent(input, Encoding.Default, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCategorie(int id)
        {
            string path = "";
            HttpResponseMessage response = await _httpClient.DeleteAsync(path);
            response.EnsureSuccessStatusCode();
        }

    }
}
