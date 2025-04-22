using Api.Dtos.Articulo;
using Api.Dtos.Common;
using Api.Dtos.Movimientos;
using Api.Dtos.Prioridad;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace APP.Services.Movimiento
{
    public class Movimientos:IMovimientos
    {
        private static string? _baseurl;
        public Movimientos()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<ResultDto<ResponsePosicionDto>> GetAll_Posicion_RelacionArticulo(string strToken, string strCodSucursal, string strCodArticulo)
        {
           var clientHttp=new HttpClient();
            clientHttp.BaseAddress =new Uri(_baseurl);
            clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clientHttp.GetAsync($"/Movimientos/GetAll_Posicion_RelacionArticulo/{strCodArticulo}/{strCodSucursal}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<ResponsePosicionDto>>>(responseContent);
                return apiResponse.Resultado;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }

        }

        public async Task<ResultDto<PrioridadDto>> GetAll_Prioridades_Mov(string strToken, int intIdRelacionArticuloPosicion)
        {
            var clientHttp = new HttpClient();
            clientHttp.BaseAddress = new Uri(_baseurl);
            clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clientHttp.GetAsync($"/Movimientos/GetAll_Prioridad_RelacionArticulo/{intIdRelacionArticuloPosicion}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<PrioridadDto>>>(responseContent);
                return apiResponse.Resultado;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<ResultDto<ResponseHeaderDtoXTipoMov>> GetAll_Search_Mov_HeaderxTipoMov(string strToken,int intIdTipoMovimiento, string strCodSucursal, string strUsuario)
        {
            var httpCLiente=new HttpClient();
            httpCLiente.BaseAddress = new Uri(_baseurl);
            httpCLiente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await httpCLiente.GetAsync($"/Movimientos/GetAll_Search_Mov_HeaderxTipoMov/{intIdTipoMovimiento}/{strCodSucursal}/{strUsuario}");
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<ResponseHeaderDtoXTipoMov>>>(responseContent);

                return apiResponse.Resultado;

            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<ResultDto<TipoMovimientoDto>> GetAll_Search_TipoMovimiento(string strToken, int intIdTipoMovimiento)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync($"/Movimientos/GetAll_Search/{intIdTipoMovimiento}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<TipoMovimientoDto>>>(responseContent);

                return apiResponse.Resultado;
               
            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<ResultDto<ResponseUnidadMedidaDto>> GetAll_UnidadMedida_RelacionArticulo(string strToken, int intIdPosicion, string strCodSucursal, string strCodArticulo)
        {
            var httpClient=new HttpClient();
            httpClient.BaseAddress = new Uri(_baseurl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await httpClient.GetAsync($"/Movimientos/GetAll_UnidadMedida_RelacionArticulo/{intIdPosicion}/{strCodSucursal}/{strCodArticulo}");
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<ResponseUnidadMedidaDto>>>(responseContent);
                return apiResponse.Resultado;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

        public async Task<ResultDto<int>> Insert_MovimientoDetail(string strToken,MovimientoDetailRequestDto movimientoDetailRequestDto)
        {

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_baseurl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var content = new StringContent(JsonConvert.SerializeObject(movimientoDetailRequestDto), System.Text.Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync($"Movimientos/InsertMovimientoDetail/",content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<int>>>(responseContent);
                return apiResponse.Resultado;

            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");

            }
         
        }

        public async Task<ResultDto<int>> Insert_MovimientoHeader(string strToken, MovimientoRequestDto movimientoRequestDto)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_baseurl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var content = new StringContent(JsonConvert.SerializeObject(movimientoRequestDto), System.Text.Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync($"Movimientos/InsertMovimiento/", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<int>>>(responseContent);
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
