using Api.Abstractions.IApplications;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Applications.User
{
    public class ApplicationsUser:IApplicationUser
    {
        private IServicesUser _iServicesUsers;

        public ApplicationsUser(IServicesUser iServicesUsers)
        {
            _iServicesUsers = iServicesUsers;
        }

        public async Task<ResultDto<UserDetailDto>> Login(string strUsuario, string strPassword)
        {
           return await _iServicesUsers.Login(strUsuario, strPassword);
        }
    }
}
