using APP.Services.Articulo;
using APP.Services.Movimiento;
using APP.Services.Posicion;
using APP.Services.Prioridad;
using APP.Services.RelacionArticulo;
using APP.Services.UnidadMedida;
using APP.Services.User;

namespace APP.Services
{
    public static class ServicesRegister
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<IUser, User.User>();
            services.AddScoped<IArticulo, Articulo.Articulo>();
            services.AddScoped<IPosicion,Posicion.Posicion>();
            services.AddScoped<IUnidadMedida,UnidadMedida.UnidadMedida>();
            services.AddScoped<IRelacionArticulo, RelacionArticulo.RelacionArticulo>();
            services.AddScoped<IPrioridad, Prioridad.Prioridad>();
            services.AddScoped<IMovimientos,Movimiento.Movimientos>();
            return services;
        
        }

    }
}
