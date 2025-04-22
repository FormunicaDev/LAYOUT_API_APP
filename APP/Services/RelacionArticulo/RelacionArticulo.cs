
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Api.Dtos.RelacionArticulo;
using Api.Dtos.UnidadMedida;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace APP.Services.RelacionArticulo
{
    public class RelacionArticulo : IRelacionArticulo
    {
        private static string? _baseurl;
        public RelacionArticulo()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<ResultDto<int>> CreateOrUpdateRelacionArticulo(string strToken, RelacionArticuloDto relacionArticuloDto)
        {
            var clientHttp=new HttpClient();
            clientHttp.BaseAddress = new Uri(_baseurl);
            clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var content = new StringContent(JsonConvert.SerializeObject(relacionArticuloDto), Encoding.UTF8, "application/json");
            var response = await clientHttp.PostAsync($"/RelacionArticulo/CreateUpdate_RelacionArticulo", content);

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

        public async Task<ResultDto<RelacionArticuloDto>> GetRelacionArticulo(string strToken, string strCodBodega)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync($"/RelacionArticulo/List_RelacionArticulo/{strCodBodega}");

            if (response.IsSuccessStatusCode)
            {
               var responseContent = await response.Content.ReadAsStringAsync();
              
               var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<RelacionArticuloDto>>> (responseContent);
                
               
               return apiResponse.Resultado;
            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<ResultDto<RelacionArticuloDto>> SearchRelacionArticulo(string strToken, int? intIdRelacionArticuloPosicion, string strCodBodega)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync($"/RelacionArticulo/Search_RelacionArticulo/{intIdRelacionArticuloPosicion}/{strCodBodega}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<RelacionArticuloDto>>>(responseContent);

                return apiResponse.Resultado;
            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<ResultDto<dynamic>> GetPosicionArticulo(string strToken, string strCodBodega, string strCodArticulo)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_baseurl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await httpClient.GetAsync($"/RelacionArticulo/GetSearchPosicionXArticulo/{strCodBodega}/{strCodArticulo}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<dynamic>>>(responseContent);

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
