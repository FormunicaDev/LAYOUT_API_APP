using Api.Abstractions.IApplications;
using Api.Applications.Articulo;
using Api.Dtos.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("Articulo")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IApplicationArticulo _applicationArticulo;

        public ArticuloController(IApplicationArticulo applicationArticulo)
        {
            this._applicationArticulo = applicationArticulo;
        }

        [Authorize]
        [HttpGet]
        [Route("ListadoArticuloUsuario")]
        public async Task<IActionResult> ListadoArticuloUsuario() {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<string> resultado = new ResultDto<string>();

            try
            {


                if (identity.Claims.Count() == 0)
                {
                    resultado.IsSuccess = false;
                    resultado.Message = "No tiene permiso contactarse con el administrador";
                    resultado.Item = null;
                    return Forbid(resultado.Message);
                }

                var usuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                var result = await _applicationArticulo.GetArticulo(Convert.ToString(usuario.ToString()));
                string jsonResponse = JsonConvert.SerializeObject(new { result });
                var content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json");
                content.Headers.ContentLength = jsonResponse.Length;
                return new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex) {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetArticulo_Posicion_NumeroParte/{strCodSucursal}/{strArticulo}")]
        public async Task<IActionResult> GetArticulo_Posicion_NumeroParte(string strCodSucursal, string strArticulo)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<string> resultado = new ResultDto<string>();

            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultado.IsSuccess = false;
                    resultado.Message = "No tiene permiso contactarse con el administrador";
                    resultado.Item = null;
                    return Forbid(resultado.Message);
                }

                var usuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                var result = await _applicationArticulo.GetArticulo_Posicion_NumeroParte(strCodSucursal, strArticulo);
                string jsonResponse = JsonConvert.SerializeObject(new { result });
                var content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json");
                content.Headers.ContentLength = jsonResponse.Length;
                return new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("SearchArticulo/{strBusqueda}")]
        public async Task<IActionResult> GetArticulo_Posicion_NumeroParte(string strBusqueda)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity; 
            ResultDto<string> resultado = new ResultDto<string>();

            if(strBusqueda=="*"){
                strBusqueda="";
            }

            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultado.IsSuccess = false;
                    resultado.Message = "No tiene permiso contactarse con el administrador";
                    resultado.Item = null;
                    return Forbid(resultado.Message);
                }

                var usuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                var result = await _applicationArticulo.SearchArticulo(strBusqueda);
                string jsonResponse = JsonConvert.SerializeObject(new { result });
                var content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json");
                content.Headers.ContentLength = jsonResponse.Length;
                return new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    

    }
}
