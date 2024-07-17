using CoreBusiness;
using System.Text;
using System.Text.Json;
using WebApp.Factory.Interfaces;

namespace WebApp.Factory
{
    public class TransactionsClient : ITransactionsClient
    {
        private readonly HttpClient _httpClient;
        public TransactionsClient(HttpClient clientFactory)
        {
            _httpClient = clientFactory;
        }

        public async Task<ViewModels.TransactionsViewModel> GetTransactions(ViewModels.TransactionsViewModel transactionsViewModel)
        {
            string path = "";
            var input = JsonSerializer.Serialize(transactionsViewModel);
            HttpResponseMessage response = await _httpClient.PostAsync(path, new StringContent(input, Encoding.Default, "application/json"));
            string data = await response.Content.ReadAsStringAsync();
            var values = JsonSerializer.Deserialize<ViewModels.TransactionsViewModel>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return values;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsToday(string name)
        {
            string path = $"/Today?cashierName={name}";
            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{path}");
            string data = await response.Content.ReadAsStringAsync();
            var values = JsonSerializer.Deserialize<IEnumerable<Transaction>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return values;
        }

    }
}
