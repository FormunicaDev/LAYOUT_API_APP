using Api.Abstractions.IApplications;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Applications.Posicion
{
    public class ApplicationPosicion:IApplicationPosicion
    {
        private readonly IServicesPosicion _IservicesPosicion;

        public ApplicationPosicion(IServicesPosicion iservicesPosicion)
        {
            _IservicesPosicion = iservicesPosicion;
        }

        public async Task<ResultDto<int>> CrearPosicion([FromForm]  PosicionDto posiciones)
        {
            return await _IservicesPosicion.CrearPosicion(posiciones);
        }

        public async Task<ResultDto<PosicionDto>> GetAll(int intIdPosicion, string strCodSucursal)
        {
            return await _IservicesPosicion.GetAll(intIdPosicion, strCodSucursal);
        }

        public async Task<ResultDto<PosicionDto>> GetSearch(int intIdPosicion, string strCodSucursal)
        {
            return await _IservicesPosicion.GetSearch(intIdPosicion, strCodSucursal);
        }

        public async Task<ResultDto<byte[]>> ImprimirPosicion(int intIdPosicion)
        {
            return await _IservicesPosicion.ImprimirPosicion(intIdPosicion);
        }

        public async Task<ResultDto<TipoPosicionDto>> Searh_GetAll_TipoPosicion(int intIdTipoPosicion)
        {
            return await _IservicesPosicion.Searh_GetAll_TipoPosicion(intIdTipoPosicion);
        }

        public async Task<ResultDto<int>> UpdatePosicion(PosicionDto posiciones)
        {
            return await _IservicesPosicion.UpdatePosicion(posiciones);
        }
    }
}
