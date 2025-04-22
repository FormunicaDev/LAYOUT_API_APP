using Api.Abstractions.IRepository;
using Api.Dtos.Common;
using Api.Dtos.RelacionArticulo;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.RelacionArticulo
{
    public class RepositoryRelacionArticulo : IRepositoryRelacionArticulo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public RepositoryRelacionArticulo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<ResultDto<int>> CreateOrUpdate(RelacionArticuloDto relacionArticuloDto)
        {
            ResultDto<int> result=new ResultDto<int>();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@intIdRelacionArticuloPosicion", relacionArticuloDto.intIdRelacionArticuloPosicion);
                    dynamicParameters.Add("@strCodArticulo", relacionArticuloDto.strCodArticulo);
                    dynamicParameters.Add("@intIdPosicion", relacionArticuloDto.intIdPosicion);
                    dynamicParameters.Add("@intIdUnidadMedida", relacionArticuloDto.intIdUnidadMedida);
                    dynamicParameters.Add("@intIdPrioridad", relacionArticuloDto.intIdPrioridad);
                    dynamicParameters.Add("@decPesoKg", relacionArticuloDto.decPesoKg);
                    dynamicParameters.Add("@decUnidadesFisica", relacionArticuloDto.decUnidadesFisica);
                    dynamicParameters.Add("@strUsuario", relacionArticuloDto.strUsuario);

                    int repuesta =await con.QuerySingleAsync<int>("LAYOUT.Sp_Create_Update_RelacionArticulo",dynamicParameters,commandType:CommandType.StoredProcedure);

                    if (repuesta > 0 && relacionArticuloDto.intIdRelacionArticuloPosicion==0) {
                        result.Message = "SE GUARDO CORRECTAMENTE";
                        result.IsSuccess = true;
                        result.Item = repuesta;
                    }
                    else if (repuesta > 0 && relacionArticuloDto.intIdRelacionArticuloPosicion>0) {
                        result.Message = "SE ACTUALIZO CORRECTAMENTE";
                        result.IsSuccess = true;
                        result.Item = repuesta;
                    }
                    else
                    {
                        result.Message = "FALLO AL GUARDAR";
                        result.IsSuccess = false;
                        result.Item = repuesta;
                    }

                    return result;
                }
                   

            }
            catch (Exception ex) {

                result.Message = ex.Message;
                result.IsSuccess = false;
                return result;
               
            }
        }

        public async Task<ResultDto<object>> GetAll(int intIdRelacionArticuloPosicion, string strCodSucursal)
        {
            ResultDto<object> result=new ResultDto<object>();

            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@intIdRelacionArticuloPosicion", intIdRelacionArticuloPosicion=0);
                dynamicParameters.Add("@strCodSucursal", strCodSucursal);
                using (var cn=new SqlConnection(_connectionString))
                {
                    var repuesta = await cn.QueryAsync<object>("[Layout].[spList_Search_Articulo]", dynamicParameters,commandType:System.Data.CommandType.StoredProcedure);
                    result.Data = repuesta.ToList();
                    result.Message = repuesta.ToList().Count() > 0 ? "Informacion Encontrada" : "La Informacion no se encontro";
                    result.IsSuccess= repuesta.ToList().Count() > 0? true : false;  
                    return result;
                }
            }
            catch (Exception ex) {
                result.MessageException= ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }

        public async Task<ResultDto<object>> GetSearch(int intIdRelacionArticuloPosicion, string strCodSucursal)
        {
            ResultDto<object> result = new ResultDto<object>();

            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@intIdRelacionArticuloPosicion", intIdRelacionArticuloPosicion);
                dynamicParameters.Add("@strCodSucursal", strCodSucursal);
                using (var cn = new SqlConnection(_connectionString))
                {
                    var repuesta = await cn.QuerySingleAsync<object>("[Layout].[spList_Search_Articulo]", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    result.Item = repuesta;
                    result.Message = repuesta != null ? "Informacion Encontrada" : "La Informacion no se encontro";
                    result.IsSuccess = repuesta != null ? true : false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.MessageException = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }

        public async Task<ResultDto<object>> GetSearchPosicionXArticulo(string strCodSucursal, string strCodArticulo)
        {
            ResultDto<object> result = new ResultDto<object>();
           try{
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@strCodSucursal",strCodSucursal);
                dynamicParameters.Add("@strCodArticulo",strCodArticulo);

                using(var cn=new SqlConnection(_connectionString)){

                        var response= await cn.QueryAsync<object>("Layout.spSearchPosicionDeArticulo", dynamicParameters,commandType:CommandType.StoredProcedure);

                        result.Data = response.ToList().Count()>0?response.ToList():null;
                        result.Message=response.ToList().Count()>0?"INFORMACION ENCONTRADA":"NO SE ENCONTRO INFORMACION";
                        result.IsSuccess=response.ToList().Count()>0?true:false;
                        return result;

                }
           }
           catch (Exception ex){
                 result.MessageException=ex.Message;
                 result.IsSuccess=false;
                 return result;

           }
        }
    }
}
