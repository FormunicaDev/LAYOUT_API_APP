using Api.Abstractions.IRepository;
using Api.Dtos.Articulo;
using Api.Dtos.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.Articulo
{
    public class RepositoryArticulo:IRepositoryArticulo
    {
        private readonly IConfiguration _configuration;
        private string _connectioString = "";
       
        public RepositoryArticulo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectioString = configuration.GetConnectionString("CadenaSQL");
           
        }

        public async Task<ResultDto<List<ArticuloDto>>> GetArticulo(string strUsuario)
        {
            var result = new ResultDto<List<ArticuloDto>>
            {
                IsSuccess = false,
                Message = "Error al obtener los artículos",
                Item = new List<ArticuloDto>()
            };

            try
            {
                using (var con = new SqlConnection(_connectioString))
                {

                    var parameter = new DynamicParameters();
                    parameter.Add("@strUsuario", strUsuario, DbType.String);


                    var articulos = await con.QueryAsync<ArticuloDto>("[Layout].[spObtenerArticulo]", parameter, commandType: CommandType.StoredProcedure);


                    result.Item = articulos.ToList();
                    result.IsSuccess = true;
                    result.Message = "Artículos obtenidos con éxito";
                }
            }
            catch (Exception ex)
            {

                result.MessageException = ex.Message;
            }

            return result;
        }

        public async Task<ResultDto<List<DetailArticuloDto>>> GetArticulo_Posicion_NumeroParte(string strCodSucursal, string strArticulo)
        {
            var result = new ResultDto<List<DetailArticuloDto>>
            {
                IsSuccess = false,
                Message = "Error al obtener los artículos",
                Item = new List<DetailArticuloDto>()
            };
            try
            {
                using (var con = new SqlConnection(_connectioString))
                {

                    var parameter = new DynamicParameters();
                    parameter.Add("@strCodBodega", strCodSucursal, DbType.String);
                    parameter.Add("@strArticulo_o_strPosicion_o_NumParte", strArticulo, DbType.String);


                    var articulos = await con.QueryAsync<DetailArticuloDto>("[Layout].[BuscarArticulo_o_Posicion]", parameter, commandType: CommandType.StoredProcedure);

                    if (articulos.IsNullOrEmpty())
                    {

                        result.IsSuccess = false;
                        result.Message = "No se encontro dato";
                        return result;
                    }
                    result.Item = articulos.ToList();
                    result.IsSuccess = true;
                    result.Message = "Artículos obtenidos con éxito";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.MessageException = ex.Message;
            }

            return result;
        }

        public async Task<ResultDto<ArticuloDto>> SearchArticulo(string strBusqueda) {
            ResultDto<ArticuloDto> result=new ResultDto<ArticuloDto>();    
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@strBusqueda", strBusqueda);

                using (var con=new SqlConnection(_connectioString)){
                     var response=await con.QueryAsync<ArticuloDto>("Layout.Sp_Busqueda_Articulo",dynamicParameters,commandType:CommandType.StoredProcedure); 

                     result.Data=response.ToList().Count()>0?response.ToList():null;
                     result.Message=response.ToList().Count()>0?"Datos Encontrados":"No se encotraron resultado";
                     result.IsSuccess = true;
                }

                return result;
              
            }
            catch (Exception ex) { 
            
                     result.Message=ex.Message;
                     result.IsSuccess = false;
                     return result;
            }

        }
    }
}
