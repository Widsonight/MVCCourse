using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Data;
namespace Auth__Classic
{
    public static class AuthClass_DepInject
    {
        public static IServiceCollection AddClassicAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("AccountContextConnection"));
            });
            return services;
        }
    }

}
