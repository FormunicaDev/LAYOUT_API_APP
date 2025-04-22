using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Api.Dtos.Prioridad;

namespace APP.Services.Prioridad
{
    public interface IPrioridad
    {
        public Task<List<PrioridadDto>> GetPrioridad(string StrToken);
    }
}
