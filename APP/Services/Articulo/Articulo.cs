using Api.Dtos.Articulo;
using Api.Dtos.Common;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace APP.Services.Articulo
{
    public class Articulo : IArticulo
    {
        private static string? _baseurl;
        public Articulo()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }
        public async Task<List<DetailArticuloDto>> GetArticulo_Posicion_NumeroParte(string strToken, string strCodSucursal, string strCodArticulo)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync($"/Articulo/GetArticulo_Posicion_NumeroParte/{strCodSucursal}/{strCodArticulo}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();


                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<List<DetailArticuloDto>>>>(responseContent);

                // Acceder a los datos dentro de "Item"
                return apiResponse.Resultado.Item;
            }

            else
            {

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<List<ArticuloDto>> ListadoArticulo(string strToken)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync("Articulo/ListadoArticuloUsuario");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();


                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<List<ArticuloDto>>>>(responseContent);

                // Acceder a los datos dentro de "Item"
                return apiResponse.Resultado.Item;
            }

            else
            {

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<List<ArticuloDto>> SearchArticulo(string strToken, string strBusqueda)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync($"Articulo/SearchArticulo/{strBusqueda}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();


                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<ArticuloDto>>>(responseContent);

                // Acceder a los datos dentro de "Item"
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
