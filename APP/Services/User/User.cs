using Api.Dtos.Common;
using Api.Dtos.User;
using Newtonsoft.Json;
using System.Security.Authentication;
using System.Text;

namespace APP.Services.User
{
    public class User : IUser
    {
        #region Variables
        private static string? _baseurl;
        #endregion

        #region Constructor
        public User()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }
        #endregion
        public async Task<ResultDto<UserDetailDto>> LoginRequest(LoginDto login)
        {
            var handler = new HttpClientHandler
                {
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
                };

            var clienteHttp = new HttpClient(handler);
            clienteHttp.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await clienteHttp.PostAsync("User/Login/", content);

            if (response.IsSuccessStatusCode)
            {

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResultDto<UserDetailDto>>(responseContent);
                return result;
            }
            else
            {

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }
    }
}
