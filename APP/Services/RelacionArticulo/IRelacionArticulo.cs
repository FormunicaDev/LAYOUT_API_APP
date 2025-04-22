using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Api.Dtos.RelacionArticulo;

namespace APP.Services.RelacionArticulo
{
    public interface IRelacionArticulo
    {
        public Task<ResultDto<RelacionArticuloDto>> GetRelacionArticulo(string strToken, string strCodBodega);
        public Task<ResultDto<RelacionArticuloDto>> SearchRelacionArticulo(string strToken, int? intIdRelacionArticuloPosicion,string strCodBodega);
        public Task<ResultDto<int>> CreateOrUpdateRelacionArticulo(string strToken, RelacionArticuloDto relacionArticuloDto);
        public Task<ResultDto<dynamic>> GetPosicionArticulo(string strToken, string strCodBodega, string strCodArticulo);
    }
}
