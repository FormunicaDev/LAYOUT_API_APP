using Api.Dtos.Common;
using Api.Dtos.Posicion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IRepository
{
    public interface IRepositoryPosicion
    {
        public Task<ResultDto<int>> CrearPosicion(PosicionDto posiciones);
        public Task<ResultDto<int>> UpdatePosicion(PosicionDto posiciones);
        public Task<ResultDto<PosicionDto>> GetAll(int intIdPosicion, string strCodSucursal);
        public Task<ResultDto<PosicionDto>> GetSearch(int intIdPosicion, string strCodSucursal);
        public Task<ResultDto<TipoPosicionDto>> Searh_GetAll_TipoPosicion(int intIdTipoPosicion);
        public Task<ResultDto<byte[]>> ImprimirPosicion(int intIdPosicion);


    }
}
