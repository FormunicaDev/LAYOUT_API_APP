using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Api.Dtos.UnidadMedida;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace APP.Services.UnidadMedida
{
    public class UnidadMedida:IUnidadMedida
    {
        private static string? _baseurl;
        public UnidadMedida()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<ResultDto<int>> CreateOrUpdate_UnidadMedida( string strToken, [FromBody] UnidadMedidaDto unidadMedidaDto)
        {
            var httpClient=new HttpClient();
            httpClient.BaseAddress = new Uri(_baseurl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",strToken);
            var content = new StringContent(JsonConvert.SerializeObject(unidadMedidaDto), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"/UnidadMedida/CreateUpdate_UnidadMedida", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<int>>>(jsonResponse);

                return res.Resultado;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }
        public async Task<ResultDto<UnidadMedidaDto>> GetUnidadMedida(string strToken, string strCodBodega)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync($"/UnidadMedida/List_UnidadMedida/{strCodBodega}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<UnidadMedidaDto>>>(responseContent);
                return apiResponse.Resultado;
            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<ResultDto<UnidadMedidaDto>> SearchUnidadMedida(string strToken, int? id)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_baseurl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await httpClient.GetAsync($"/UnidadMedida/Search_UnidadMedida/{id}/{"*"}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<UnidadMedidaDto>>>(responseContent);
                Console.WriteLine(apiResponse.Resultado.Data);
                return apiResponse.Resultado;
            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }

        }

    }
}
