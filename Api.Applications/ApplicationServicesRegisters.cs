using Api.Abstractions.IApplications;
using Api.Applications.Articulo;
using Api.Applications.Bodega;
using Api.Applications.Movimientos;
using Api.Applications.Posicion;
using Api.Applications.Prioridad;
using Api.Applications.RelacionArticulo;
using Api.Applications.UnidadMedida;
using Api.Applications.User;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Applications
{
    public static class ApplicationServicesRegisters
    {
        public static IServiceCollection AddApplicationsServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUser, ApplicationsUser>();
            services.AddScoped<IApplicationBodega, ApplicationsBodega>();
            services.AddScoped<IApplicationArticulo, ApplicationArticulo>();
            services.AddScoped<IApplicationPosicion, ApplicationPosicion>();
            services.AddScoped<IApplicationUnidadMedida,ApplicationUnidadMedida>();
            services.AddScoped<IApplicationRelacionArticulo,ApplicationRelacionArticulo>();
            services.AddScoped<IApplicationPrioridad, ApplicationsPrioridad>();
            services.AddScoped<IApplicationMovimiento, ApplicationMovimientos>();
            return services;
        }
    }
}
