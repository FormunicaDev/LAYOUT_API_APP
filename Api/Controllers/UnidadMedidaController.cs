using Api.Abstractions.IApplications;
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Api.Dtos.UnidadMedida;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("UnidadMedida")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly IApplicationUnidadMedida applicationUnidadMedida;

        public UnidadMedidaController(IApplicationUnidadMedida applicationUnidadMedida)
        {
            this.applicationUnidadMedida = applicationUnidadMedida;
        }


        [Authorize]
        [HttpPost]
        [Route("CreateUpdate_UnidadMedida")]
        public async Task<ActionResult> CreateUpdate_UnidadMedida([FromBody] UnidadMedidaDto unidadMedidaDto)
        { 
         var identity=HttpContext.User.Identity as ClaimsIdentity;
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


                var strUsuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                unidadMedidaDto.strUsuario = strUsuario;
                var result = await applicationUnidadMedida.CrearUpdate_UnidadMedida(unidadMedidaDto);
                string JsonResponse = JsonConvert.SerializeObject(new { result });
                var Content = new StringContent(JsonResponse, System.Text.Encoding.UTF8, "application/json");
                Content.Headers.ContentLength = JsonResponse.Length;

                return new ContentResult
                {
                    Content = JsonResponse,
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
        [Route("List_UnidadMedida/{strCodSucursal}")]
        public async Task<ActionResult> List_UnidadMedida(string strCodSucursal)
        {
            var identity=HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<UnidadMedidaDto> unidadMedidaDto = new ResultDto<UnidadMedidaDto>();
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    unidadMedidaDto.Message = "NO TIENE PERMISO COMUNICARSE CON EL ADMINISTRADOR";
                    unidadMedidaDto.IsSuccess= false;
                    return Forbid(unidadMedidaDto.Message);
                }
                if (unidadMedidaDto.Item == null)
                {
                    unidadMedidaDto.Item = new UnidadMedidaDto (); // Asegúrate de usar la clase correspondiente
                }
                var strUsuario = identity.Claims.FirstOrDefault(x=>x.Type== "strUsuario").Value;
                unidadMedidaDto.Item.strUsuario = strUsuario.ToString();
                var result = await applicationUnidadMedida.GetAll(0, strCodSucursal);
                var JsonResult = JsonConvert.SerializeObject(new { result });
                var Content = new StringContent(JsonResult, System.Text.Encoding.UTF8, "application/json");

                Content.Headers.ContentLength = JsonResult.Length;

                return new ContentResult
                {
                    Content = JsonResult,
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
        [Route("Search_UnidadMedida/{intIdUnidadMedida}/{strCodSucursal}")]
        public async Task<ActionResult> Search_UnidadMedida(int intIdUnidadMedida,string strCodSucursal)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<UnidadMedidaDto> unidadMedidaDto = new ResultDto<UnidadMedidaDto>();
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    unidadMedidaDto.Message = "NO TIENE PERMISO COMUNICARSE CON EL ADMINISTRADOR";
                    unidadMedidaDto.IsSuccess = false;
                    return Forbid(unidadMedidaDto.Message);
                }
                if (unidadMedidaDto.Item == null)
                {
                    unidadMedidaDto.Item = new UnidadMedidaDto(); // Asegúrate de usar la clase correspondiente
                }
                var strUsuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                unidadMedidaDto.Item.strUsuario = strUsuario.ToString();
                var result = await applicationUnidadMedida.GetSearch(intIdUnidadMedida, "*");
                var JsonResult = JsonConvert.SerializeObject(new { result });
                var Content = new StringContent(JsonResult, System.Text.Encoding.UTF8, "application/json");

                Content.Headers.ContentLength = JsonResult.Length;

                return new ContentResult
                {
                    Content = JsonResult,
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
        [Route("GetSearchUnidadMedidaArticulo/{intIdRelacionArticuloPosicion}")]
        public async Task<ActionResult> GetSearchUnidadMedidaArticulo(int intIdRelacionArticuloPosicion)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<UnidadMedidaDto> unidadMedidaDto = new ResultDto<UnidadMedidaDto>();
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    unidadMedidaDto.Message = "NO TIENE PERMISO COMUNICARSE CON EL ADMINISTRADOR";
                    unidadMedidaDto.IsSuccess = false;
                    return Forbid(unidadMedidaDto.Message);
                }
                if (unidadMedidaDto.Item == null)
                {
                    unidadMedidaDto.Item = new UnidadMedidaDto(); // Asegúrate de usar la clase correspondiente
                }
                var strUsuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                unidadMedidaDto.Item.strUsuario = strUsuario.ToString();
                var result = await applicationUnidadMedida.GetSearchUnidadMedidaArticulo(intIdRelacionArticuloPosicion);
                var JsonResult = JsonConvert.SerializeObject(new { result });
                var Content = new StringContent(JsonResult, System.Text.Encoding.UTF8, "application/json");

                Content.Headers.ContentLength = JsonResult.Length;

                return new ContentResult
                {
                    Content = JsonResult,
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
