using Api.Dtos.Common;
using Api.Dtos.Movimientos;
using Api.Dtos.Prioridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IServices
{
    public interface IServicesMovimiento
    {
        public Task<ResultDto<int>> InsertMovimiento(MovimientoRequestDto movimientoRequestDto);
        public Task<ResultDto<int>> InsertMovimientoDetail(MovimientoDetailRequestDto movimientoRequestDto);
        public Task<ResultDto<TipoMovimientoDto>> GetAll_Search_TipoMovimiento(int intIdTipoMovimiento);
        public Task<ResultDto<ResponsePosicionDto>> GetAll_Posicion_RelacionArticulo(RequestPosicionDto requestPosicionDto);
        public Task<ResultDto<ResponseUnidadMedidaDto>> GetAll_UnidadMedida_RelacionArticulo(int intIdPosicion, string strCodSucursal,string strCodArticulo);
        public Task<ResultDto<ResponseHeaderDtoXTipoMov>> GetAll_Search_Mov_HeaderxTipoMov(int intIdTipoMovimiento, string strCodSucursal,string strUsuario);
        public Task<ResultDto<PrioridadDto>> GetAll_Prioridad_RelacioArticuloMov(int intIdRelacionArticuloPosicion);
    }
}
