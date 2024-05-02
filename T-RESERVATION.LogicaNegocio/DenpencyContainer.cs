using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using T_RESERVATION.AccesoDatos;

namespace T_RESERVATION.LogicaNegocio;

public static class DenpencyContainer
{

    public static IServiceCollection AddBLDependecies(this IServiceCollection services, 
    IConfiguration configuration){

        services.AddDalDependecides(configuration);
        services.AddScoped<ClienteBL>();
        services.AddScoped<EmpleadosBL>();
        services.AddScoped<MenuBL>();
        return services;
    }
}

