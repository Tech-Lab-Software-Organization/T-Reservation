using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using T_RESERVATION.EntidadesNegocio;

namespace T_RESERVATION.AccesoDatos;

public static class DenpencyContainer
{
     public static IServiceCollection AddDalDependecides(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("conn")));

        services.AddScoped<ClientesDAL>();
        services.AddScoped<EmpleadoDAL>();
        services.AddScoped<MenuDAL>();
        services.AddScoped<RestaurantesDAL>();

        return services;
    }

}