using Api.Abstractions.IRepository;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.User
{
    public class ServicesUser : IServicesUser
    {
        private IRepositoryUser _repositoryUsers;
        public ServicesUser(IRepositoryUser repositoryUsers)
        {
            _repositoryUsers = repositoryUsers;
        }
        public async Task<ResultDto<UserDetailDto>> Login(string strUsuario, string strPassword)
        {
            return await _repositoryUsers.Login(strUsuario, strPassword);
        }
    }
}
