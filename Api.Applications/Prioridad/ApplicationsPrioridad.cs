using Api.Abstractions.IApplications;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.Prioridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Applications.Prioridad
{
    public class ApplicationsPrioridad:IApplicationPrioridad
    {
        private readonly IServicesPrioridad _servicesPrioridad;

        public ApplicationsPrioridad(IServicesPrioridad servicesPrioridad)
        {
            _servicesPrioridad = servicesPrioridad;
        }

        public async Task<ResultDto<PrioridadDto>> GetAll()
        {
            return await _servicesPrioridad.GetAll();
        }
    }
}
