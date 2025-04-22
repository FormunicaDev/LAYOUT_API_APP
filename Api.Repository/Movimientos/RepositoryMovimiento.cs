using Api.Abstractions.IRepository;
using Api.Dtos.Common;
using Api.Dtos.Movimientos;
using Api.Dtos.Prioridad;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.Movimientos
{
    public class RepositoryMovimiento : IRepositoryMovimiento
    {
        public string _connectionString="";
        public RepositoryMovimiento(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<ResultDto<ResponsePosicionDto>> GetAll_Posicion_RelacionArticulo(RequestPosicionDto requestPosicionDto)
        {
            ResultDto<ResponsePosicionDto> result = new ResultDto<ResponsePosicionDto>();

            try{
                 
                DynamicParameters dynamicParameters=new DynamicParameters();
                using (var con = new SqlConnection(_connectionString)){
                    dynamicParameters.Add("@strCodArticulo",requestPosicionDto.strCodArticulo);
                    dynamicParameters.Add("@strCodSucursal",requestPosicionDto.strCodSucursal);

                    var repuesta=await con.QueryAsync<ResponsePosicionDto>("[Layout].[sp_GetPosicionPorIdRelacionArticulo]",dynamicParameters,commandType:CommandType.StoredProcedure);

                    result.Data=repuesta.ToList().Count()>0?repuesta.ToList():null;
                    result.IsSuccess=repuesta.ToList().Count()>0?true:false;
                    result.Message=repuesta.ToList().Count()>0?"INFORMACION ENCONTRADA":"NO SE ENCONTRO";
                    return result;
                }
            }
            catch(Exception ex){
                    result.MessageException=ex.Message;
                    result.IsSuccess=false; 
                    return result;
            }
        }

        public async Task<ResultDto<PrioridadDto>> GetAll_Prioridad_RelacioArticuloMov(int intIdRelacionArticuloPosicion)
        {
            ResultDto<PrioridadDto> result=new ResultDto<PrioridadDto>();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@intIdRelacionArticuloPosicion", intIdRelacionArticuloPosicion);
                    var repuest = await con.QueryAsync<PrioridadDto>("layout.Sp_BuscarPrioridad_RelacionArticulo", dynamicParameters, commandType: CommandType.StoredProcedure);

                    result.Item = repuest.ToList().Count() > 0 ? repuest.ToList().FirstOrDefault() : null;
                    result.Message = repuest.ToList().Count() > 0 ? "LA INFORMACION SE ENCONTRO" : "NO SE ENCONTRO LA INFORMACION";
                    result.IsSuccess = true;
                    return result;
                }
            }
            catch (Exception ex) { 
                result.MessageException=ex.Message;
                result.IsSuccess=false; 
                return result;
            }
        }

        public async Task<ResultDto<ResponseHeaderDtoXTipoMov>> GetAll_Search_Mov_HeaderxTipoMov(int intIdTipoMovimiento, string strCodSucursal,string strUsuario)
        {
            ResultDto<ResponseHeaderDtoXTipoMov> result = new ResultDto<ResponseHeaderDtoXTipoMov>();
            try{
                    using(var con=new SqlConnection(_connectionString)){
                        DynamicParameters dynamicParameters=new DynamicParameters();
                        dynamicParameters.Add("@strCodSucursal",strCodSucursal);
                        dynamicParameters.Add("@intIdTipoMovimiento",intIdTipoMovimiento);
                        dynamicParameters.Add("@strUsuario",strUsuario);

                        var response= await con.QueryAsync<ResponseHeaderDtoXTipoMov>("layout.sp_GetAll_Mov_Header_TipoMovimiento", dynamicParameters,commandType:CommandType.StoredProcedure);

                        result.Data=response.ToList().Count()>0?response.ToList():null;
                        result.IsSuccess = response.ToList().Count()>0?true:false;
                        result.Message=response.ToList().Count()>0?"INFORMACION ENCONTRADA":"NO SE ENCONTRO LA INFORMACION";
                        return result;
                    }
            }catch(Exception ex){
                    result.IsSuccess=false;
                    result.MessageException=ex.Message;
                    return result;
            }
        }

        public async Task<ResultDto<TipoMovimientoDto>> GetAll_Search_TipoMovimiento(int intIdTipoMovimiento)
        {
            ResultDto<TipoMovimientoDto> result = new ResultDto<TipoMovimientoDto>();
            try
            {
                using (var con=new SqlConnection(_connectionString))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@intIdTipoMovimiento", intIdTipoMovimiento);

                    var response =await con.QueryAsync<TipoMovimientoDto>("LAYOUT.sp_List_Search_TipoMovimiento", dynamicParameters, commandType: CommandType.StoredProcedure);


                    result.Item=response.ToList().Count==1?response.FirstOrDefault():null;
                    result.Data = response.ToList().Count > 0 ? response.ToList() : null;
                    result.Message = response.ToList().Count > 0 ?"INFORMACION ENCONTRADA":"NO SE ENCONTRO LA INFORMACION";
                    result.IsSuccess= response.ToList().Count > 0?true : false;
                    return result;
                }
            }
            catch (Exception ex) { 
                result.MessageException=ex.Message;
                result.IsSuccess=false;
                return result;
            }
        }

        public async Task<ResultDto<ResponseUnidadMedidaDto>> GetAll_UnidadMedida_RelacionArticulo(int intIdPosicion, string strCodSucursal,string strCodArticulo)
        {
             ResultDto<ResponseUnidadMedidaDto> result = new ResultDto<ResponseUnidadMedidaDto>();

            try{
                 
                DynamicParameters dynamicParameters=new DynamicParameters();
                using (var con = new SqlConnection(_connectionString)){
                    dynamicParameters.Add("@intIdPosicion",intIdPosicion);
                    dynamicParameters.Add("@strCodSucursal",strCodSucursal);
                    dynamicParameters.Add("@strCodArticulo",strCodArticulo);

                    var repuesta=await con.QueryAsync<ResponseUnidadMedidaDto>("[Layout].[sp_GetUnidadMedidaPorIdRelacionArticulo]",dynamicParameters,commandType:CommandType.StoredProcedure);

                    result.Data=repuesta.ToList().Count()>0?repuesta.ToList():null;
                    result.IsSuccess=repuesta.ToList().Count()>0?true:false;
                    result.Message=repuesta.ToList().Count()>0?"INFORMACION ENCONTRADA":"NO SE ENCONTRO";
                    return result;
                }
            }
            catch(Exception ex){
                    result.MessageException=ex.Message;
                    result.IsSuccess=false; 
                    return result;
            }
        }

        public async Task<ResultDto<int>> InsertMovimiento(MovimientoRequestDto movimientoRequestDto)
        {
           ResultDto<int> result=new ResultDto<int>();
            try
            {
                using (var con=new SqlConnection(_connectionString))
                {
                    DynamicParameters dynamicParameters=new DynamicParameters();
                    dynamicParameters.Add("@strDescripcion", movimientoRequestDto.strDescripcion);
                    dynamicParameters.Add("@strNumeroDocumento", movimientoRequestDto.strNumeroDocumento);
                    dynamicParameters.Add("@intIdTipoMovimiento", movimientoRequestDto.intIdTipoMovimiento);
                    dynamicParameters.Add("@strUsuario", movimientoRequestDto.strUsuario);
                    int response = await con.ExecuteScalarAsync<int>("Layout.Sp_Insert_MovimientoArticulo", dynamicParameters, commandType: CommandType.StoredProcedure);
                    
                    result.Item=response;
                    result.Message = response != null ? "SE GUARDO CORRECTAMENTE" : "NO SE GUARDO";
                    result.IsSuccess = response != null ? true:false;
                    return result;

                }

            }
            catch (Exception ex) {
                result.IsSuccess=false;
                result.MessageException=ex.Message;
                return result;
            }
        }

        public async Task<ResultDto<int>> InsertMovimientoDetail(MovimientoDetailRequestDto movimientoRequestDto)
        {
            ResultDto<int> result = new ResultDto<int>();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@decCantidad", movimientoRequestDto.decCantidad);
                    dynamicParameters.Add("@intIdMovimientoArticulo", movimientoRequestDto.intIdMovimientoArticulo);
                    dynamicParameters.Add("@intIdRelacionArticuloPosicion", movimientoRequestDto.intIdRelacionArticuloPosicion);
                    dynamicParameters.Add("@strUsuario", movimientoRequestDto.strUsuario);
                    int response = await con.ExecuteScalarAsync<int>("Layout.Sp_Insert_MovimientoArticuloDetalles", dynamicParameters, commandType: CommandType.StoredProcedure);

                    result.Item = response;
                    result.Message = response != null ? "SE GUARDO CORRECTAMENTE" : "NO SE GUARDO";
                    result.IsSuccess = response != null ? true : false;
                    return result;

                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.MessageException = ex.Message;
                return result;
            }
        }


       
    }
}
