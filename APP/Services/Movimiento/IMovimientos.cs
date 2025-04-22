using Api.Dtos.Articulo;
using Api.Dtos.Common;
using Api.Dtos.Movimientos;
using Api.Dtos.Prioridad;

namespace APP.Services.Movimiento
{
    public interface IMovimientos
    {
        public Task<ResultDto<TipoMovimientoDto>> GetAll_Search_TipoMovimiento(string strToken, int intIdTipoMovimiento);
        public Task<ResultDto<ResponsePosicionDto>> GetAll_Posicion_RelacionArticulo(string strToken, string strCodSucursal,string strCodArticulo);
        public Task<ResultDto<ResponseUnidadMedidaDto>> GetAll_UnidadMedida_RelacionArticulo(string strToken, int intIdPosicion, string strCodSucursal,string strCodArticulo);
        public Task<ResultDto<int>> Insert_MovimientoHeader(string strToken, MovimientoRequestDto movimientoRequestDto);
        public Task<ResultDto<int>> Insert_MovimientoDetail(string strToken, MovimientoDetailRequestDto movimientoDetailRequestDto);
        public Task<ResultDto<ResponseHeaderDtoXTipoMov>> GetAll_Search_Mov_HeaderxTipoMov(string strToken, int intIdTipoMovimiento, string strCodSucursal,string strUsuario);
        public Task<ResultDto<PrioridadDto>> GetAll_Prioridades_Mov(string strToken, int intIdRelacionArticuloPosicion);
    }
}
