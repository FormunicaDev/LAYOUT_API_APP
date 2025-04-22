using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Api.Dtos.RelacionArticulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IApplications
{
    public interface IApplicationRelacionArticulo
    {
        public Task<ResultDto<object>> GetAll(int intIdRelacionArticuloPosicion, string strCodSucursal);
        public Task<ResultDto<object>> GetSearch(int intIdRelacionArticuloPosicion, string strCodSucursal);
        public Task<ResultDto<int>> CreateOrUpdate(RelacionArticuloDto relacionArticuloDto);
         public Task<ResultDto<object>>GetSearchPosicionXArticulo(string strCodSucursal,string strCodArticulo);
    }
}
