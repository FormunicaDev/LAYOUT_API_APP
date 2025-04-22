using Api.Abstractions.IRepository;
using Api.Dtos.Common;
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

namespace Api.Repository.Prioridad
{
    public class RepositoryPrioridad:IRepositoryPrioridad
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public RepositoryPrioridad(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<ResultDto<PrioridadDto>> GetAll()
        {
           ResultDto<PrioridadDto> result=new ResultDto<PrioridadDto>();
            try
            {
                using (var con=new SqlConnection(_connectionString))
                {
                     var repuesta = await con.QueryAsync<PrioridadDto>("Layout.Sp_List_Search_Prioridad", null, commandType: CommandType.StoredProcedure);

                    result.Data = repuesta.ToList();
                    result.IsSuccess = repuesta.ToList().Count()>0?true:false;
                    result.Message = repuesta.ToList().Count() > 0 ? "SE ENCONTRO DATOS" : "NO SE ENCONTRO DATOS";
                    return result;
                }
            }
            catch (Exception ex) {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
