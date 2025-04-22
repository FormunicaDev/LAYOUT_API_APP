using Api.Abstractions.IApplications;
using Api.Abstractions.IServices;
using Api.Dtos.Bodega;
using Api.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Applications.Bodega
{
    public class ApplicationsBodega:IApplicationBodega
    {
        private IServicesBodega _IservicesBodega;
        public ApplicationsBodega(IServicesBodega servicesBodega)
        {
            _IservicesBodega = servicesBodega;
        }

        public async Task<ResultDto<List<BodegaDto>>> ListadoBodegaUsuario(string strUsuario)
        {
            return await _IservicesBodega.ListadoBodegaUsuario(strUsuario);
        }
    }
}
