using Api.Dtos.Common;
using Api.Dtos.Prioridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IRepository
{
    public interface IRepositoryPrioridad
    {
        public Task<ResultDto<PrioridadDto>> GetAll();
    }
}
