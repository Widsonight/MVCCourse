using IdentityModel.Client;

namespace WebApp.Factory.Interfaces
{
    public interface ITokenClient
    {
        Task<TokenResponse> getToken();
    }
}