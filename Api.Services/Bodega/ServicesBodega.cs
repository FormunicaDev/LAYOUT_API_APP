using Api.Abstractions.IRepository;
using Api.Abstractions.IServices;
using Api.Dtos.Bodega;
using Api.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Bodega
{
    public class ServicesBodega:IServicesBodega
    {
        private IRepositoryBodega _IrepositoryBodega;

        public ServicesBodega(IRepositoryBodega repositoryBodega)
        {
            _IrepositoryBodega = repositoryBodega;
        }

        public async Task<ResultDto<List<BodegaDto>>> ListadoBodegaUsuario(string strUsuario)
        {
            return await _IrepositoryBodega.ListadoBodegaUsuario(strUsuario);
        }
    }
}
