using Api.Dtos.Common;
using Api.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IServices
{
    public interface IServicesUser
    {
        public Task<ResultDto<UserDetailDto>> Login(string strUsuario, string strPassword);
    }
}
