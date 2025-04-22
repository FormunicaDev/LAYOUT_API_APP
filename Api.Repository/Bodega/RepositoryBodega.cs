using Api.Abstractions.IRepository;
using Api.Dtos.Bodega;
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

namespace Api.Repository.Bodega
{
    public class RepositoryBodega:IRepositoryBodega
    {
        private readonly IConfiguration _configuration;
        private string _connectioString = "";

        public RepositoryBodega(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectioString = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<ResultDto<List<BodegaDto>>> ListadoBodegaUsuario(string strUsuario)
        {
            ResultDto<List<BodegaDto>> result = new ResultDto<List<BodegaDto>>();
            List<BodegaDto> list = new List<BodegaDto>();
            try
            {
                using (var con = new SqlConnection(_connectioString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@strUsuario", strUsuario);

                    var bodega = await con.QueryAsync<BodegaDto>("LAYOUT.spSucursalUsuario", parameters, commandType: CommandType.StoredProcedure);

                    if (bodega.IsNullOrEmpty())
                    {
                        result.IsSuccess = false;
                        result.MessageException = "Error al Mostrar";
                    }
                    else
                    {
                        result.Message = "Resultado Correcto";
                        result.IsSuccess = true;
                        result.Item = bodega.ToList();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.MessageException = ex.Message;
            }
            return result;
        }
    }
}
