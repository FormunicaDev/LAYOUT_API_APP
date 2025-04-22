using Api.Abstractions.IApplications;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.RelacionArticulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Applications.RelacionArticulo
{

    public class ApplicationRelacionArticulo:IApplicationRelacionArticulo
    {
        private readonly IServicesRelacionArticulo servicesRelacionArticulo;

        public ApplicationRelacionArticulo(IServicesRelacionArticulo servicesRelacionArticulo)
        {
            this.servicesRelacionArticulo = servicesRelacionArticulo;
        }

        public async Task<ResultDto<int>> CreateOrUpdate(RelacionArticuloDto relacionArticuloDto)
        {
            return await servicesRelacionArticulo.CreateOrUpdate(relacionArticuloDto);
        }

        public async Task<ResultDto<object>> GetAll(int intIdRelacionArticuloPosicion, string strCodSucursal)
        {
            return await servicesRelacionArticulo.GetAll(intIdRelacionArticuloPosicion,strCodSucursal);
        }

        public async Task<ResultDto<object>> GetSearch(int intIdRelacionArticuloPosicion, string strCodSucursal)
        {
            return await servicesRelacionArticulo.GetSearch(intIdRelacionArticuloPosicion, strCodSucursal);
        }

        public async Task<ResultDto<object>> GetSearchPosicionXArticulo(string strCodSucursal, string strCodArticulo)
        {
            return await servicesRelacionArticulo.GetSearchPosicionXArticulo(strCodSucursal,strCodArticulo);
        }
    }
}
