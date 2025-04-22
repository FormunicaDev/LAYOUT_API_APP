using Api.Abstractions.IRepository;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.UnidadMedida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.UnidadMedida
{
    public class ServicesUnidadMedida:IServicesUnidadMedida
    {
        private readonly IRepositoryUnidadMedida _IRepositoryUnidadMedida;

        public ServicesUnidadMedida(IRepositoryUnidadMedida IRepositoryUnidadMedida)
        {
            _IRepositoryUnidadMedida = IRepositoryUnidadMedida;
        }

        public async Task<ResultDto<int>> CrearUpdate_UnidadMedida(UnidadMedidaDto unidadMedidaDto)
        {
            return await _IRepositoryUnidadMedida.CrearUpdate_UnidadMedida(unidadMedidaDto);
        }

        public async Task<ResultDto<UnidadMedidaDto>> GetAll(int intIdUnidadMedida, string strCodSucursal)
        {
            return await _IRepositoryUnidadMedida.GetAll(intIdUnidadMedida, strCodSucursal);
        }

        public async Task<ResultDto<UnidadMedidaDto>> GetSearch(int intIdUnidadMedida, string strCodSucursal)
        {
            return await _IRepositoryUnidadMedida.GetSearch(intIdUnidadMedida, strCodSucursal);
        }

        public async Task<ResultDto<dynamic>> GetSearchUnidadMedidaArticulo(int intIdRelacionArticuloPosicion)
        {
            return await _IRepositoryUnidadMedida.GetSearchUnidadMedidaArticulo(intIdRelacionArticuloPosicion);
        }
    }
}
