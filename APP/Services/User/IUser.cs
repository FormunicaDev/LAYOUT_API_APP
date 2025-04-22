using Api.Dtos.Common;
using Api.Dtos.User;

namespace APP.Services.User
{
    public interface IUser
    {
        public Task<ResultDto<UserDetailDto>> LoginRequest(LoginDto login);
    }
}

