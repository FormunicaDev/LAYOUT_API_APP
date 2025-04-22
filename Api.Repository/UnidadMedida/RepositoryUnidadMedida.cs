using Api.Abstractions.IRepository;
using Api.Dtos.Common;
using Api.Dtos.UnidadMedida;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.UnidadMedida
{
    public class RepositoryUnidadMedida : IRepositoryUnidadMedida
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public RepositoryUnidadMedida(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<ResultDto<int>> CrearUpdate_UnidadMedida(UnidadMedidaDto unidadMedidaDto)
        {
            ResultDto<int> result=new ResultDto<int>();

            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@intIdUnidadMedida", unidadMedidaDto.intIdUnidadMedida);
                dynamicParameters.Add("@strUnidadMedida", unidadMedidaDto.strUnidadMedida);
                dynamicParameters.Add("@decCantidadUnidad", unidadMedidaDto.decCantidadUnidad);
                dynamicParameters.Add("@strCodSucursal", unidadMedidaDto.strCodSucursal);
                dynamicParameters.Add("@strUsuario", unidadMedidaDto.strUsuario);
                using (var con=new SqlConnection(_connectionString))
                {
                    int intIdUnidadMedida = await con.QuerySingleAsync<int>("Layout.Sp_Create_Update_UnidaMedida", dynamicParameters,commandType:CommandType.StoredProcedure);
                    result.Item=intIdUnidadMedida;
                    result.Message= intIdUnidadMedida > 0 ? "SE CREO CORRECTAMENTE" : "NO SE CREO CORRECTAMENTE";
                    result.IsSuccess = intIdUnidadMedida > 0 ? true : false;
                    return result;
                }
            }
            catch (Exception ex) {
                result.MessageException=ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }

        public async Task<ResultDto<UnidadMedidaDto>> GetAll(int intIdUnidadMedida, string strCodSucursal)
        {
            ResultDto<UnidadMedidaDto> result = new ResultDto<UnidadMedidaDto>();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters=new DynamicParameters();
                    parameters.Add("@intIdUnidadMedida", intIdUnidadMedida=0);
                    parameters.Add("@strCodSucursal", strCodSucursal);

                    var repuesta = await con.QueryAsync<UnidadMedidaDto>("Layout.Sp_List_Search_UnidadMedida",parameters,commandType:CommandType.StoredProcedure);
                    result.Data=repuesta.ToList();
                    result.Message = repuesta.ToList().Count() > 0 ? "INFORMACION ENCONTRADA" : "NO SE ENCONTRO LA INFORMACION";
                    result.IsSuccess = repuesta.ToList().Count() > 0 ? true : false;
                    return result;

                }
               
            }
            catch (Exception ex) {
                result.MessageException = "";
                result.IsSuccess = false;
                return result;
            }
            
        }

        public async Task<ResultDto<UnidadMedidaDto>> GetSearch(int intIdUnidadMedida, string strCodSucursal)
        {
            ResultDto<UnidadMedidaDto> result = new ResultDto<UnidadMedidaDto>();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@intIdUnidadMedida", intIdUnidadMedida);
                    parameters.Add("@strCodSucursal", strCodSucursal="*");

                    var repuesta = await con.QueryAsync<UnidadMedidaDto>("[Layout].[Sp_List_Search_UnidadMedida]", parameters, commandType: CommandType.StoredProcedure);
                    result.Item = repuesta.FirstOrDefault();
                    result.Message = "Datos Encontrados";
                    result.IsSuccess = true;
                    return result;

                }

            }
            catch (Exception ex)
            {
                result.MessageException = "";
                result.IsSuccess = false;
                return result;
            }
        }

        public async Task<ResultDto<dynamic>> GetSearchUnidadMedidaArticulo(int intIdRelacionArticuloPosicion)
        {
            ResultDto<dynamic> result = new ResultDto<dynamic>();
            try{

                using (var con = new SqlConnection(_connectionString)){
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@intIdRelacionArticuloPosicion",intIdRelacionArticuloPosicion);

                    var repuesta = await con.QueryAsync("layout.spSearchUnidadMedidaArticulo", parameters, commandType:CommandType.StoredProcedure);

                    result.Data=repuesta.ToList().Count()>0 ? repuesta.ToList():null;
                    result.Message = repuesta.ToList().Count()>0?"SE ENCONTRO INFORMACION":"NO SE ENCONTRO INFORMACION";
                    result.IsSuccess = repuesta.ToList().Count()>0?true:false;
                    return result; 
                }

            }
            catch(Exception ex){
                result.MessageException = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }

        
        }
}
