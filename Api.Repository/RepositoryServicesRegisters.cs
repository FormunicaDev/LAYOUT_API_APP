using Api.Abstractions.IRepository;
using Api.Repository.Articulo;
using Api.Repository.Bodega;
using Api.Repository.Movimientos;
using Api.Repository.Posicion;
using Api.Repository.Prioridad;
using Api.Repository.RelacionArticulo;
using Api.Repository.UnidadMedida;
using Api.Repository.User;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Repository
{
    public static class RepositoryServicesRegisters
    {
        public static IServiceCollection AddRepositoryRegisters(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IRepositoryBodega,RepositoryBodega>();
            services.AddScoped<IRepositoryArticulo, RepositoryArticulo>();
            services.AddScoped<IRepositoryPosicion, RepositoryPosicion>();
            services.AddScoped<IRepositoryUnidadMedida,RepositoryUnidadMedida>();
            services.AddScoped<IRepositoryRelacionArticulo, RepositoryRelacionArticulo>();
            services.AddScoped<IRepositoryPrioridad, RepositoryPrioridad>();
            services.AddScoped<IRepositoryMovimiento,RepositoryMovimiento>();
            return services;
        }
    }
}
