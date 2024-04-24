using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace T_RESERVATION.AccesoDatos;

public static class DenpencyContainer
{
     public static IServiceCollection AddDalDependecides(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("conn")));

        services.AddScoped<>();

        return services;
    }

}