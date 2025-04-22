using Api.Abstractions.IRepository;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services.Posicion
{
    public class ServicesPosicion:IServicesPosicion
    {
        private readonly IRepositoryPosicion _IrepositoryPosicion;

        public ServicesPosicion(IRepositoryPosicion irepositoryPosicion)
        {
            _IrepositoryPosicion = irepositoryPosicion;
        }

        public async Task<ResultDto<int>> CrearPosicion([FromForm] PosicionDto posiciones)
        {
           return await _IrepositoryPosicion.CrearPosicion(posiciones);
        }

        public async Task<ResultDto<PosicionDto>> GetAll(int intIdPosicion, string strCodSucursal)
        {
            return await _IrepositoryPosicion.GetAll(intIdPosicion, strCodSucursal);
        }

        public async Task<ResultDto<PosicionDto>> GetSearch(int intIdPosicion, string strCodSucursal)
        {
            return await _IrepositoryPosicion.GetSearch(intIdPosicion, strCodSucursal);
        }

        public async Task<ResultDto<byte[]>> ImprimirPosicion(int intIdPosicion)
        {
            return await _IrepositoryPosicion.ImprimirPosicion(intIdPosicion);
        }

        public async Task<ResultDto<TipoPosicionDto>> Searh_GetAll_TipoPosicion(int intIdTipoPosicion)
        {
            return await _IrepositoryPosicion.Searh_GetAll_TipoPosicion(intIdTipoPosicion);
        }

        public async Task<ResultDto<int>> UpdatePosicion(PosicionDto posiciones)
        {
           return await _IrepositoryPosicion.UpdatePosicion(posiciones);
        }
    }
}
