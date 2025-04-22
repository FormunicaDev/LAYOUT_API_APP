using Api.Dtos.Common;
using Api.Dtos.Prioridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IServices
{
    public interface IServicesPrioridad
    {
        public Task<ResultDto<PrioridadDto>> GetAll();
    }
}
