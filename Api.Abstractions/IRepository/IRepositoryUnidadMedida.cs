using Api.Dtos.Common;
using Api.Dtos.UnidadMedida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IRepository
{
    public interface IRepositoryUnidadMedida
    {
        public Task<ResultDto<UnidadMedidaDto>> GetAll(int intIdUnidadMedida, string strCodSucursal);
        public Task<ResultDto<UnidadMedidaDto>> GetSearch(int intIdUnidadMedida, string strCodSucursal);
        public Task<ResultDto<int>> CrearUpdate_UnidadMedida(UnidadMedidaDto unidadMedidaDto);
        public  Task<ResultDto<dynamic>> GetSearchUnidadMedidaArticulo(int intIdRelacionArticuloPosicion);


      
    }
}
