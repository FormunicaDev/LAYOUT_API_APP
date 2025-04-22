using Api.Abstractions.IRepository;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.Prioridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Prioridad
{
    public class ServicesPrioridad:IServicesPrioridad
    {
        private readonly IRepositoryPrioridad _repositoryPrioridad;

        public ServicesPrioridad(IRepositoryPrioridad repositoryPrioridad)
        {
            _repositoryPrioridad = repositoryPrioridad;
        }

        public async Task<ResultDto<PrioridadDto>> GetAll()
        {
            return await _repositoryPrioridad.GetAll();
        }
    }
}
