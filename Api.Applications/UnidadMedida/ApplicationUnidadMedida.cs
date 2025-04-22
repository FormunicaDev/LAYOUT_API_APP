using Api.Abstractions.IApplications;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.UnidadMedida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Applications.UnidadMedida
{
    public class ApplicationUnidadMedida:IApplicationUnidadMedida
    {
        private readonly IServicesUnidadMedida servicesUnidadMedida;

        public ApplicationUnidadMedida(IServicesUnidadMedida servicesUnidadMedida)
        {
            this.servicesUnidadMedida = servicesUnidadMedida;
        }

        public async Task<ResultDto<int>> CrearUpdate_UnidadMedida(UnidadMedidaDto unidadMedidaDto)
        {
            return await servicesUnidadMedida.CrearUpdate_UnidadMedida(unidadMedidaDto);
        }

        public async Task<ResultDto<UnidadMedidaDto>> GetAll(int intIdUnidadMedida, string strCodSucursal)
        {
            return await servicesUnidadMedida.GetAll(intIdUnidadMedida, strCodSucursal);
        }

        public async Task<ResultDto<UnidadMedidaDto>> GetSearch(int intIdUnidadMedida, string strCodSucursal)
        {
            return await servicesUnidadMedida.GetSearch(intIdUnidadMedida, strCodSucursal);
        }

        public async Task<ResultDto<dynamic>> GetSearchUnidadMedidaArticulo(int intIdRelacionArticuloPosicion)
        {
            return await servicesUnidadMedida.GetSearchUnidadMedidaArticulo(intIdRelacionArticuloPosicion);
        }
    }
}
