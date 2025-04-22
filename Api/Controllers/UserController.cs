using Api.Abstractions.IApplications;
using Api.Dtos.Common;
using Api.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IApplicationUser applicationUsers;
        public UserController(IApplicationUser applicationUsers)
        {
            this.applicationUsers = applicationUsers;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto UserData)
        {
            if (UserData == null || string.IsNullOrEmpty(UserData.strUsuario) || string.IsNullOrEmpty(UserData.strPassword))
            {
                var errorResult = new ResultDto<string>
                {
                    IsSuccess = false,
                    Message = "Usuario o contraseña no pueden estar vacíos."
                };


                string errorJsonResponse = JsonConvert.SerializeObject(errorResult);
                return new ContentResult
                {
                    Content = errorJsonResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var loginResult = await applicationUsers.Login(UserData.strUsuario, UserData.strPassword);

            if (!loginResult.IsSuccess)
            {
                var errorResult = new ResultDto<string>
                {
                    IsSuccess = false,
                    Message = "Usuario o contraseña Invalida"
                };


                string errorJsonResponse = JsonConvert.SerializeObject(errorResult);
                return new ContentResult
                {
                    Content = errorJsonResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else
            {

                string loginJsonResponse = JsonConvert.SerializeObject(loginResult);
                return new ContentResult
                {
                    Content = loginJsonResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }
    }
}
