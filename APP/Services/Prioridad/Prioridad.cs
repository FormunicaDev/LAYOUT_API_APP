using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Api.Dtos.Prioridad;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace APP.Services.Prioridad
{
    public class Prioridad : IPrioridad
    {
        private static string? _baseurl;
        public Prioridad()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }
        public async Task<List<PrioridadDto>> GetPrioridad(string StrToken)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StrToken);
            var response = await clienteHttp.GetAsync($"/Prioridad/List_Prioridad");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<PrioridadDto>>>(responseContent);
                return apiResponse.Resultado.Data;
            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }
    }
}
