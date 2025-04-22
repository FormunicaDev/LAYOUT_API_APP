using Api.Dtos.Common;
using Api.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IRepository
{
    public interface IRepositoryUser
    {
        public Task<ResultDto<UserDetailDto>> Login(string strUsuario, string strPassword);
        public Task<List<UserActionsDto>> ValidarAccesoUsuario(int intIdRol);
        public Task<int> ValidarUsuario(string strUsuario, string strPassword);
        public Task<UserRolDto> ValidarRol(string strUsuario);
        public Task<ResultDto<UserDetailDto>> ValidarToken(ClaimsIdentity identity);
    }
}
