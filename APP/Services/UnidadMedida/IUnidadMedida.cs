
using Api.Dtos.Common;
using Api.Dtos.UnidadMedida;
using Microsoft.AspNetCore.Mvc;

namespace APP.Services.UnidadMedida
{
    public interface IUnidadMedida
    {
        public Task<ResultDto<int>> CreateOrUpdate_UnidadMedida(string strToken, [FromBody] UnidadMedidaDto unidadMedidaDto);
        public Task<ResultDto<UnidadMedidaDto>> GetUnidadMedida(string strToken, string strCodBodega);
        public Task<ResultDto<UnidadMedidaDto>> SearchUnidadMedida(string strToken, int? id);
    }
}
