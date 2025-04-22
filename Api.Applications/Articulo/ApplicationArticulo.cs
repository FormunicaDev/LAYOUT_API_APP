using Api.Abstractions.IApplications;
using Api.Abstractions.IServices;
using Api.Dtos.Articulo;
using Api.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Applications.Articulo
{
    public class ApplicationArticulo : IApplicationArticulo
    {
        private readonly IServicesArticulo _IservicesArticulo;

        public ApplicationArticulo(IServicesArticulo iservicesArticulo)
        {
            _IservicesArticulo = iservicesArticulo;
        }

        public async Task<ResultDto<List<ArticuloDto>>> GetArticulo(string strUsuario)
        {
           return await _IservicesArticulo.GetArticulo(strUsuario);
        }

        public async Task<ResultDto<List<DetailArticuloDto>>> GetArticulo_Posicion_NumeroParte(string strCodSucursal, string strArticulo)
        {
            return await _IservicesArticulo.GetArticulo_Posicion_NumeroParte(strCodSucursal,strArticulo);
        }

        public async Task<ResultDto<ArticuloDto>> SearchArticulo(string strBusqueda)
        {
            return await _IservicesArticulo.SearchArticulo(strBusqueda);
        }
    }
}
