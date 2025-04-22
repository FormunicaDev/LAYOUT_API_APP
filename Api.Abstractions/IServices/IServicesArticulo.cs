using Api.Dtos.Articulo;
using Api.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IServices
{
    public interface IServicesArticulo
    {
        public Task<ResultDto<List<ArticuloDto>>> GetArticulo(string strUsuario);
        public Task<ResultDto<List<DetailArticuloDto>>> GetArticulo_Posicion_NumeroParte(string strCodSucursal, string strArticulo);
        public Task<ResultDto<ArticuloDto>> SearchArticulo(string strBusqueda);
    }
}
