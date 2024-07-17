using IdentityModel.Client;
using System.Net.Http;
using WebApp.Factory.Interfaces;

namespace WebApp.Factory
{
    public class TokenFacClient : ITokenClient
    {

        private readonly HttpClient _httpClient;
        public TokenFacClient(HttpClient tokenFactory)
        {
            _httpClient = tokenFactory;

        }

        public async Task<TokenResponse> getToken()
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync("");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                throw new Exception(disco.Error);
            }
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.WriteLine(tokenResponse.ErrorDescription);
                throw new Exception(tokenResponse.ErrorDescription);
            }

            Console.WriteLine(tokenResponse.AccessToken);
            return tokenResponse;
        }
    }
}
