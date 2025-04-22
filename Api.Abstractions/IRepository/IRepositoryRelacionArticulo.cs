using Api.Dtos.Common;
using Api.Dtos.RelacionArticulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IRepository
{
    public interface IRepositoryRelacionArticulo
    {
        public Task<ResultDto<object>> GetAll(int intIdRelacionArticuloPosicion, string strCodSucursal);
        public Task<ResultDto<object>> GetSearch(int intIdRelacionArticuloPosicion, string strCodSucursal);
        public Task<ResultDto<int>> CreateOrUpdate(RelacionArticuloDto relacionArticuloDto);
         public Task<ResultDto<object>>GetSearchPosicionXArticulo(string strCodSucursal,string strCodArticulo);
    }
}
