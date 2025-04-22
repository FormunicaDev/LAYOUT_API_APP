using Api.Abstractions.IApplications;
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Api.Controllers
{
    [Route("Posicion")]
    [ApiController]
    public class PosicionController : ControllerBase
    {
        #region DECLARACION VARIABLE Y CONSTRUCTOR
            private readonly IApplicationPosicion _IApplicationPosicion;

            public PosicionController(IApplicationPosicion iApplicationPosicion)
            {
                _IApplicationPosicion = iApplicationPosicion;
            }
        #endregion

    
        #region CREATE POSITION NEW
            [Authorize]
            [HttpPost]
            [Route("Create_Posicion")]
            public async Task<ActionResult> Create_Posicion([FromForm] PosicionDto posicionDto)
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
                posicionDto.strUsuario = strUsuario;
                var result=await _IApplicationPosicion.CrearPosicion(posicionDto);
                string JsonResponse = JsonConvert.SerializeObject(new { result});
                var Content=new StringContent(JsonResponse,System.Text.Encoding.UTF8,"application/json");
                Content.Headers.ContentLength=JsonResponse.Length;

                return new ContentResult
                {
                    Content = JsonResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex) {

                return BadRequest(ex.Message);
            }
        }
        #endregion 


        #region UPDATE POSITION

            [Authorize]
            [HttpPost]
            [Route("Update_Posicion")]
            [DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
            public async Task<ActionResult> Update_Posicion([FromForm] PosicionDto posicionDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<string> resultado = new ResultDto<string>();
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultado.IsSuccess = false;
                    resultado.Message = "NO TIENE PERMISO COMUNICARSE CON EL ADMINISTRADOR";
                    resultado.Item = null;
                    return Forbid(resultado.Message);
                }


                var strUsuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                posicionDto.strUsuario = strUsuario;
                var result = await _IApplicationPosicion.UpdatePosicion(posicionDto);
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


        #region LIST POSITION
            [Authorize]
            [HttpGet]
            [Route("List_Posicion/{strCodSucursal}")]
            public async Task<ActionResult> List_Posicion(string strCodSucursal)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<PosicionDto> resultado = new ResultDto<PosicionDto>();

            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultado.IsSuccess = false;
                    resultado.Message = "No tiene permiso contactarse con el administrador";
                    resultado.Item = null;
                    return Forbid(resultado.Message);
                }

                var result = await _IApplicationPosicion.GetAll(0,strCodSucursal);

                var JsonResult=JsonConvert.SerializeObject(new { result });

                var Content=new StringContent(JsonResult,System.Text.Encoding.UTF8,"application/json");

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


        #region  SEARCH_POSITION
            [Authorize]
            [HttpGet]
            [Route("Search_Posicion/{intIdPosicion}/{strCodSucursal}")]
            public async Task<ActionResult> Search_Posicion(int intIdPosicion, string strCodSucursal)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<PosicionDto> resultado = new ResultDto<PosicionDto>();

            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultado.IsSuccess = false;
                    resultado.Message = "No tiene permiso contactarse con el administrador";
                    resultado.Item = null;
                    return Forbid(resultado.Message);
                }

                var result = await _IApplicationPosicion.GetSearch(intIdPosicion, strCodSucursal);

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
        

        #region  CREATED PDF CODE BAR
        [HttpGet]
        [Route("DescargarPdf/{intIdPosicion}")]
        public async Task<ActionResult> DescargarPdf(int intIdPosicion)
        {
          
            try
            {
               

                // Llamar al método que genera el PDF
                var pdfBytes = await _IApplicationPosicion.ImprimirPosicion(intIdPosicion);

                return File(pdfBytes.Item.ToArray(), "application/pdf");

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region  SEARCH AND LIST TIPOPOSICION
        [HttpGet]
        [Route("Search_List_TipoPosicion/{intIdTipoPosicion}")]
        public async Task<ActionResult> Searh_GetAll_TipoPosicion(int intIdTipoPosicion)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<TipoPosicionDto> resultado = new ResultDto<TipoPosicionDto>();

            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultado.IsSuccess = false;
                    resultado.Message = "No tiene permiso contactarse con el administrador";
                    resultado.Item = null;
                    return Forbid(resultado.Message);
                }

                var result = await _IApplicationPosicion.Searh_GetAll_TipoPosicion(intIdTipoPosicion);

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
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}
