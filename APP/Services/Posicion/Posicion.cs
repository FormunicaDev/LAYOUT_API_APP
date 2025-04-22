using Api.Dtos.Articulo;
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace APP.Services.Posicion
{
    public class Posicion:IPosicion
    {
        private static string? _baseurl;
        public Posicion()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<ResultDto<int>> CreateOrUpdatePosicion(string strToken, PosicionDto posicion)
        {
            ResultDto<int> result = new ResultDto<int>();
            var httpClient=new HttpClient();
            httpClient.BaseAddress = new Uri(_baseurl);
            httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer", strToken);
           // var content = new StringContent(JsonConvert.SerializeObject(posicion), Encoding.UTF8, "application/json");

            if (string.IsNullOrEmpty(posicion.strDescripcion))
            {
                posicion.strDescripcion = "No hay Datos";
            }

            string nuevaRuta = $"\\\\10.10.0.8\\ImgPosiciones\\{posicion.strPosicion}_{posicion.strCodSucursal}";

            using (var multipart = new MultipartFormDataContent())
            {
                multipart.Add(new StringContent(posicion.intIdPosicion.ToString()), name: "intIdPosicion");
                multipart.Add(new StringContent(posicion.strPosicion), name: "strPosicion");
                multipart.Add(new StringContent(posicion.strCodSucursal), name: "strCodSucursal");
                multipart.Add(new StringContent(posicion.strUsuario), name: "strUsuario");
                multipart.Add(new StringContent(posicion.strDescripcion, Encoding.UTF8), "strDescripcion");
                multipart.Add(new StringContent(nuevaRuta), name: "imgCodigoBarra");
                var fileStreamContent = new StreamContent(posicion.imgArchivo.OpenReadStream());
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(posicion.imgArchivo.ContentType);
                multipart.Add(fileStreamContent, name: "imgArchivo", fileName: posicion.imgArchivo.FileName);
                multipart.Add(new StringContent(posicion.intIdTipoPosicion.ToString()), name: "intIdTipoPosicion");
                multipart.Add(new StringContent(posicion.decCapacidad.ToString()), name: "decCapacidad");

                Console.WriteLine($"intIdPosicion: {posicion.intIdPosicion}");
                Console.WriteLine($"strPosicion: {posicion.strPosicion}");
                Console.WriteLine($"strCodSucursal: {posicion.strCodSucursal}");
                Console.WriteLine($"strUsuario: {posicion.strUsuario}");
                Console.WriteLine($"imgCodigoBarra: {posicion.imgCodigoBarra}");
                if (posicion.imgArchivo != null)
                {
                    Console.WriteLine($"imgArchivo FileName: {posicion.imgArchivo.FileName}");
                    Console.WriteLine($"imgArchivo ContentType: {posicion.imgArchivo.ContentType}");
                }
                else
                {
                    Console.WriteLine("imgArchivo es nulo");
                }

                var response = await httpClient.PostAsync("/Posicion/Update_Posicion/", multipart);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var res= JsonConvert.DeserializeObject <ResponseWrapper< ResultDto<int>>> (jsonResponse);
                    
                    return res.Resultado;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
                }
            }


         
        }

        public async Task<List<PosicionDto>> GetPosicion(string strToken, string strCodBodega)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync($"/Posicion/List_Posicion/{strCodBodega}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<PosicionDto>>>(responseContent);
                return apiResponse.Resultado.Data;
            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }
        }

      

        public async Task<PosicionDto> SearchPosicion(string strToken, int? id)
        {
            var httpClient=new HttpClient();
            httpClient.BaseAddress = new Uri(_baseurl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await httpClient.GetAsync($"/Posicion/Search_Posicion/{id}/{"*"}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<PosicionDto>>>(responseContent);

                return apiResponse.Resultado.Item;
            }

            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode}, Detalles: {errorContent}");
            }

        }

        public async Task<ResultDto<TipoPosicionDto>> Search_GetAll_TipoPosicion(string strToken, int intIdTipoPosicion)
        {
            var clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(_baseurl);
            clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
            var response = await clienteHttp.GetAsync($"/Posicion/Search_List_TipoPosicion/{intIdTipoPosicion}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Deserializar considerando el nivel "result"
                var apiResponse = JsonConvert.DeserializeObject<ResponseWrapper<ResultDto<TipoPosicionDto>>>(responseContent);
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
