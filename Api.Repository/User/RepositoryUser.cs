using Api.Abstractions.IRepository;
using Api.Dtos.Bodega;
using Api.Dtos.Common;
using Api.Dtos.User;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.User
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly IConfiguration _configuration;
        private string _connectioString = "";
        public RepositoryUser(IConfiguration configuration) {
            _configuration = configuration;
            _connectioString = configuration.GetConnectionString("CadenaSQL");
        }
        public async Task<ResultDto<UserDetailDto>> Login(string strUsuario, string strPassword)
        {
            ResultDto<int> result = new ResultDto<int>();
            int existe = await ValidarUsuario(strUsuario, strPassword);                        // Valida si el usuario es correcto
            UserRolDto usersRol = await ValidarRol(strUsuario);                                 // Obtine  el rol atravez del usuario
            List<UserActionsDto> userAcciones = await ValidarAccesoUsuario(usersRol.intIdRol); // Obtiene la acciones atravez del Rol
            List<BodegaDto> bodegasdto = await ValidarSucursalUsuario(strUsuario);
            ResultDto<UserDetailDto> dato;

            if (existe == 1)
            {
                if (usersRol.intIdRol >= 8200 && usersRol.intIdRol <= 8299)
                {
                    var jwt = _configuration.GetSection("Jwt").Get<Jwt>();                  // Extrae todo lo que esta Jwt en Appsetting.json

                    var claims = new[]                                                      // Creacion de los Claims para el token
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                        new Claim("strUsuario",strUsuario),
                    };


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));   // Pasa el key
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Configuracion de tipo de cifrado que hara el token
                    var Token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddDays(2),
                        signingCredentials: signIn
                        );

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenString = tokenHandler.WriteToken(Token);

                    Console.WriteLine(tokenString);


                    dato = new ResultDto<UserDetailDto>
                    {
                        Item = new UserDetailDto
                        {
                            strUsuario = strUsuario,
                            Acciones = userAcciones,
                            Rol = usersRol,
                            Bodegas = bodegasdto,
                            strToken = tokenString

                        },
                        Message = "Sesion exitosa"
                        ,
                        Data = new List<UserDetailDto>()
                    };
                    dato.IsSuccess = true;
                    return dato;

                }
                else
                {
                    dato = new ResultDto<UserDetailDto>
                    {

                    };

                    dato.IsSuccess = false;
                }
            }
            else
            {
                dato = new ResultDto<UserDetailDto>
                {

                };

                dato.IsSuccess = false;
                dato.MessageException = "No se puedo Iniciar sesion";
            }
            return dato;
        }

        public async Task<List<UserActionsDto>> ValidarAccesoUsuario(int intIdRol)
        {
            List<UserActionsDto> list = new List<UserActionsDto>();
            try
            {
                using (var Cn = new SqlConnection(_connectioString))
                { 
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@intIdRol", intIdRol);

                    var Actions = await Cn.QueryAsync<UserActionsDto>("Layout.spAccionesUsuario", parameters, commandType: CommandType.StoredProcedure);

                    if (!Actions.IsNullOrEmpty())
                    {
                        return Actions.ToList();
                    }

                    return Actions.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserRolDto> ValidarRol(string strUsuario)
        {
           UserRolDto userRol = new UserRolDto();
            try
            {
                using (var Cn = new SqlConnection(_connectioString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@strUsuario", strUsuario);
                    userRol.intIdRol = await Cn.QuerySingleAsync<int> ("Layout.spRolUsuario",parameters,commandType:CommandType.StoredProcedure);
                    return userRol;
                }
            }
            catch (Exception ex) {
                return null;
            }
        }

        public async Task<ResultDto<UserDetailDto>> ValidarToken(ClaimsIdentity identity)
        {
            ResultDto<UserDetailDto> result = new ResultDto<UserDetailDto> { };
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    result.Message = "Valida el acceso con Informatica";
                    result.IsSuccess = false;
                    return result;
                }

                var strUsuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                result.IsSuccess = true;
                result.Message = "Exito";
                result.Item.strUsuario = strUsuario;

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.MessageException = "Ocurrio un error" + ex.Message;
                return result;
            }
            return result;
        }

        public async Task<int> ValidarUsuario(string strUsuario, string strPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(strUsuario) || string.IsNullOrEmpty(strPassword))
                {
                    return 0;

                }

                using (var cn = new SqlConnection(_connectioString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@strUsuario", strUsuario);
                    parameters.Add("@strPassword", strPassword);

                    using (var lectura = await cn.ExecuteReaderAsync("Layout.spInicioSesion", parameters, commandType: CommandType.StoredProcedure))
                    {
                        while (lectura.Read())
                        {
                            return lectura["intIsValido"].ToString() == "1" ? 1 : 0;
                        }


                    }
                }

                return 0;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<BodegaDto>> ValidarSucursalUsuario(string strUsuario)
        {

            List<BodegaDto> list = new List<BodegaDto>();
            try
            {
                using (var Cn = new SqlConnection(_connectioString))
                {
                    await Cn.OpenAsync();


                    using (var command = new SqlCommand("Layout.spObtenerSucursalUsuario", Cn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@strUsuario", strUsuario);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var bodega = new BodegaDto
                                {
                                    strBodega = reader["strBodega"].ToString(),
                                    strCodBodega = reader["strCodBodega"].ToString()

                                };
                                list.Add(bodega);
                            }

                        }

                        return list.ToList(); ;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
