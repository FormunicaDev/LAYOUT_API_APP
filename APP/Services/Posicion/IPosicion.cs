using Api.Dtos.Common;
using Api.Dtos.Posicion;

namespace APP.Services.Posicion
{
    public interface IPosicion
    {
        public Task<List<PosicionDto>> GetPosicion(string strToken, string strCodBodega);
        public Task<PosicionDto> SearchPosicion(string strToken, int? id);
        public Task<ResultDto<int>> CreateOrUpdatePosicion(string strToken,PosicionDto posicion);
       public Task<ResultDto<TipoPosicionDto>> Search_GetAll_TipoPosicion(string strToken, int intIdTipoPosicion);

    }  
        
}
