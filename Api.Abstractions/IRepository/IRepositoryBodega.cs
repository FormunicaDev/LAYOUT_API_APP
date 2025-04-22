using Api.Dtos.Bodega;
using Api.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IRepository
{
    public interface IRepositoryBodega
    {
        public Task<ResultDto<List<BodegaDto>>> ListadoBodegaUsuario(string strUsuario);
    }
}
