using Api.Abstractions.IServices;
using Api.Services.Articulo;
using Api.Services.Bodega;
using Api.Services.Movimientos;
using Api.Services.Posicion;
using Api.Services.Prioridad;
using Api.Services.RelacionArticulo;
using Api.Services.UnidadMedida;
using Api.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Services
{
    public static class ServicesServicesRegisters
    {
        public static IServiceCollection AddServicesServices(this IServiceCollection services) { 
            
            services.AddScoped<IServicesUser,ServicesUser>();
            services.AddScoped<IServicesBodega,ServicesBodega>();
            services.AddScoped<IServicesArticulo, ServicesArticulo>();
            services.AddScoped<IServicesPosicion, ServicesPosicion>();
            services.AddScoped<IServicesUnidadMedida,ServicesUnidadMedida>();
            services.AddScoped<IServicesRelacionArticulo, ServicesRelacionArticulo>();
            services.AddScoped<IServicesPrioridad, ServicesPrioridad>();
            services.AddScoped<IServicesMovimiento,ServicesMovimientos>();
            return services;
        }
    }
}
