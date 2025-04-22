using Api.Abstractions.IRepository;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.Movimientos;
using Api.Dtos.Prioridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Movimientos
{
    public class ServicesMovimientos : IServicesMovimiento
    {
        public IRepositoryMovimiento _repositoryMovimiento;
        public ServicesMovimientos(IRepositoryMovimiento repositoryMovimiento) { 
            _repositoryMovimiento = repositoryMovimiento;
        }

        public async Task<ResultDto<ResponsePosicionDto>> GetAll_Posicion_RelacionArticulo(RequestPosicionDto requestPosicionDto)
        {
            return await _repositoryMovimiento.GetAll_Posicion_RelacionArticulo(requestPosicionDto);
        }

        public async Task<ResultDto<PrioridadDto>> GetAll_Prioridad_RelacioArticuloMov(int intIdRelacionArticuloPosicion)
        {
            return await _repositoryMovimiento.GetAll_Prioridad_RelacioArticuloMov(intIdRelacionArticuloPosicion);
        }

        public async Task<ResultDto<ResponseHeaderDtoXTipoMov>> GetAll_Search_Mov_HeaderxTipoMov(int intIdTipoMovimiento, string strCodSucursal,string strUsuario)
        {
            return await _repositoryMovimiento.GetAll_Search_Mov_HeaderxTipoMov(intIdTipoMovimiento, strCodSucursal,strUsuario);
        }

        public async Task<ResultDto<TipoMovimientoDto>> GetAll_Search_TipoMovimiento(int intIdTipoMovimiento)
        {
            return await _repositoryMovimiento.GetAll_Search_TipoMovimiento(intIdTipoMovimiento);
        }

        public async Task<ResultDto<ResponseUnidadMedidaDto>> GetAll_UnidadMedida_RelacionArticulo(int intIdPosicion, string strCodSucursal,string strCodArticulo)
        {
            return await _repositoryMovimiento.GetAll_UnidadMedida_RelacionArticulo(intIdPosicion,strCodSucursal,strCodArticulo);
        }

        public async Task<ResultDto<int>> InsertMovimiento(MovimientoRequestDto movimientoRequestDto)
        {
            return await _repositoryMovimiento.InsertMovimiento(movimientoRequestDto);
        }

        public async Task<ResultDto<int>> InsertMovimientoDetail(MovimientoDetailRequestDto movimientoRequestDto)
        {
            return await _repositoryMovimiento.InsertMovimientoDetail(movimientoRequestDto);
        }
    }
}
