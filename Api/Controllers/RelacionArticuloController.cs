using Api.Abstractions.IApplications;
using Api.Applications.UnidadMedida;
using Api.Dtos.Common;
using Api.Dtos.RelacionArticulo;
using Api.Dtos.UnidadMedida;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("RelacionArticulo")]
    [ApiController]
    public class RelacionArticuloController : ControllerBase
    {
        #region CONSTRUCTOR Y VARIABLES 
            private IApplicationRelacionArticulo _IApplicationRelacionArticulo;
        
            public RelacionArticuloController(IApplicationRelacionArticulo iApplicationRelacionArticulo,IConfiguration configuration)
            {
                _IApplicationRelacionArticulo = iApplicationRelacionArticulo;
            
            }
        #endregion CONSTRUCTOR Y VARIABLES 


        #region CREATE OR UPDATE RELACION ARTICULO
        [Authorize]
        [HttpPost]
        [Route("CreateUpdate_RelacionArticulo")]
        public async Task<ActionResult> CreateUpdate_RelacionArticulo([FromBody] RelacionArticuloDto relacionArticuloDto)
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


                var strUsuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                relacionArticuloDto.strUsuario = strUsuario;
                var result = await _IApplicationRelacionArticulo.CreateOrUpdate(relacionArticuloDto);
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

        #endregion


        #region LIST RELACION ARTICULO PARAMETERS { CODSUCURSAL}
            [Authorize] 
            [HttpGet]
            [Route("List_RelacionArticulo/{strCodSucursal}")]
            public async Task<IActionResult> List_RelacionArticulo(string strCodSucursal)
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

                var result = await _IApplicationRelacionArticulo.GetAll(0, strCodSucursal);
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
        #endregion LIST RELACIION ARTICULO PARAM { CODSUCURSAL}
        
        
        #region OBTIENE O BUSCA POSICION POR ARTICULO
            [Authorize] 
            [HttpGet]
            [Route("GetSearchPosicionXArticulo/{strCodSucursal}/{strCodArticulo}")]
            public async Task<IActionResult> GetSearchPosicionXArticulo(string strCodSucursal,string strCodArticulo)
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

                var result = await _IApplicationRelacionArticulo.GetSearchPosicionXArticulo(strCodSucursal,strCodArticulo);
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
        #endregion
        
        
        #region SEARCH_RELACIONARTICULO BUSCA UN RELACION ARTICULO 
        [Authorize]
        [HttpGet]
        [Route("Search_RelacionArticulo/{intIdRelacionArticuloPosicion}/{strCodSucursal}")]
        public async Task<IActionResult> Search_RelacionArticulo(int intIdRelacionArticuloPosicion, string strCodSucursal)
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

                var result = await _IApplicationRelacionArticulo.GetSearch (intIdRelacionArticuloPosicion, strCodSucursal);
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
        #endregion

    }
}
