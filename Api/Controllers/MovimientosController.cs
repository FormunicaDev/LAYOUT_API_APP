using Api.Abstractions.IApplications;
using Api.Dtos.Common;
using Api.Dtos.Movimientos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("Movimientos")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IApplicationMovimiento _applicationMovimiento;
        public MovimientosController(IApplicationMovimiento applicationMovimiento)
        {
            _applicationMovimiento = applicationMovimiento;
        }

        [Authorize]
        [HttpPost]
        [Route("InsertMovimiento")]
        public async Task<ActionResult> InsertMovimiento(MovimientoRequestDto movimientoRequestDto)
        {
            ResultDto<string> resultado = new ResultDto<string>();
            try
            {
                var identity=HttpContext.User.Identity as ClaimsIdentity;

                if (identity.Claims.Count() == 0)
                {
                    resultado.IsSuccess = false;
                    resultado.Message = "No tiene permiso contactarse con el administrador";
                    resultado.Item = null;
                    return Forbid(resultado.Message);
                }

                var usuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario").Value;
                var result = await _applicationMovimiento.InsertMovimiento(movimientoRequestDto);
                var jsonResponse = JsonConvert.SerializeObject(new { result });
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
        [HttpPost]
        [Route("InsertMovimientoDetail")]
        public async Task<ActionResult> InsertMovimientoDetail(MovimientoDetailRequestDto movimientoDetailRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<string> resultDto = new ResultDto<string>();
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultDto.Data = null;
                    resultDto.IsSuccess = false;
                    resultDto.Message = "No tiene permiso contactarse con el administrador";
                    return BadRequest(resultDto);
                }

                var usuario=identity.Claims.FirstOrDefault(x=>x.Type=="strUsuario");
                var result = await _applicationMovimiento.InsertMovimientoDetail(movimientoDetailRequest);

                var jsonReponse = JsonConvert.SerializeObject(new { result });
                var content=new StringContent(jsonReponse,System.Text.Encoding.UTF8,"application/json");
                content.Headers.ContentLength=jsonReponse.Length;
                return new ContentResult
                {
                    Content = jsonReponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }


        [Authorize]
        [HttpGet]
        [Route("GetAll_Search/{intIdTipoMovimiento}")]
        public async Task<ActionResult> GetAll_Search(int intIdTipoMovimiento)
        {
                var identity=HttpContext.User.Identity as ClaimsIdentity;
                ResultDto<TipoMovimientoDto> resultDto = new ResultDto<TipoMovimientoDto>();

            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultDto.Data = null;
                    resultDto.IsSuccess = false;
                    resultDto.Message = "No tiene permiso contactarse con el administrador";
                    return BadRequest(resultDto);
                }

                var usuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario");
                var result = await _applicationMovimiento.GetAll_Search_TipoMovimiento(intIdTipoMovimiento);

                var jsonReponse = JsonConvert.SerializeObject(new { result });
                var content = new StringContent(jsonReponse, System.Text.Encoding.UTF8, "application/json");
                content.Headers.ContentLength = jsonReponse.Length;
                return new ContentResult
                {
                    Content = jsonReponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize]
        [HttpGet]
        [Route("GetAll_Posicion_RelacionArticulo/{strCodArticulo}/{strCodSucursal}")]
        public async Task<ActionResult> GetAll_Posicion_RelacionArticulo(string strCodArticulo,string strCodSucursal){

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<ResponsePosicionDto> resultDto = new ResultDto<ResponsePosicionDto>();
            RequestPosicionDto request=new RequestPosicionDto();
            request.strCodArticulo=strCodArticulo;
            request.strCodSucursal=strCodSucursal;
            try{
                if(identity.Claims.Count()==0){
                    resultDto.Data = null;
                    resultDto.IsSuccess = false;
                    resultDto.Message = "No tiene permiso contactarse con el administrador";
                    return BadRequest(resultDto);
                }

                var usuario=identity.Claims.FirstOrDefault(e=>e.Type=="strUsuario");
                var result=await _applicationMovimiento.GetAll_Posicion_RelacionArticulo(request);
                var jsonResponse=JsonConvert.SerializeObject(new{result});

                var content=new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json");
                content.Headers.ContentLength = jsonResponse.Length;

                return new ContentResult{
                    Content = jsonResponse,
                    ContentType="application/json",
                    StatusCode=StatusCodes.Status200OK
                };
            }
            catch (Exception ex) {

                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("GetAll_UnidadMedida_RelacionArticulo/{intIdPosicion}/{strCodSucursal}/{strCodArticulo}")]
        public async Task<ActionResult> GetAll_UnidadMedida_RelacionArticulo(int intIdPosicion,string strCodSucursal,string strCodArticulo){
            var identity=HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<ResponseUnidadMedidaDto> resultDto = new ResultDto<ResponseUnidadMedidaDto>();
                try{
                        if(identity.Claims.Count()==0){
                            resultDto.Data = null;
                            resultDto.IsSuccess = false;
                            resultDto.Message = "No tiene permiso contactarse con el administrador";
                            return BadRequest(resultDto);
                        }

                        var usuario = identity.Claims.FirstOrDefault(x => x.Type=="strUsuario");
                        var result=await _applicationMovimiento.GetAll_UnidadMedida_RelacionArticulo(intIdPosicion,strCodSucursal,strCodArticulo);

                        var jsonReponse=JsonConvert.SerializeObject(new {result});

                        var content=new StringContent(jsonReponse,System.Text.Encoding.UTF8,"application/json");

                        return new ContentResult{
                            Content=jsonReponse,
                            ContentType="application/json",
                            StatusCode=StatusCodes.Status200OK
                        };

                }
                catch (Exception ex) {

                    return BadRequest(ex.Message);
                }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll_Search_Mov_HeaderxTipoMov/{intIdTipoMovimiento}/{strCodSucursal}/{strUsuario}")]
        public async Task<ActionResult> GetAll_Search_Mov_HeaderxTipoMov(int intIdTipoMovimiento,string strCodSucursal,string strUsuario){
            var identity=HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<ResponseUnidadMedidaDto> resultDto = new ResultDto<ResponseUnidadMedidaDto>();
                try{
                        if(identity.Claims.Count()==0){
                            resultDto.Data = null;
                            resultDto.IsSuccess = false;
                            resultDto.Message = "No tiene permiso contactarse con el administrador";
                            return BadRequest(resultDto);
                        }

                        var usuario = identity.Claims.FirstOrDefault(x => x.Type=="strUsuario");
                        var result=await _applicationMovimiento.GetAll_Search_Mov_HeaderxTipoMov(intIdTipoMovimiento,strCodSucursal,strUsuario);

                        var jsonReponse=JsonConvert.SerializeObject(new {result});

                        var content=new StringContent(jsonReponse,System.Text.Encoding.UTF8,"application/json");

                        return new ContentResult{
                            Content=jsonReponse,
                            ContentType="application/json",
                            StatusCode=StatusCodes.Status200OK
                        };

                }
                catch (Exception ex) {

                    return BadRequest(ex.Message);
                }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll_Prioridad_RelacionArticulo/{intIdRelacionArticuloPosicion}")]
        public async Task<ActionResult> GetAll_Prioridad_RelacionArticulo(int intIdRelacionArticuloPosicion)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ResultDto<ResponseUnidadMedidaDto> resultDto = new ResultDto<ResponseUnidadMedidaDto>();
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    resultDto.Data = null;
                    resultDto.IsSuccess = false;
                    resultDto.Message = "No tiene permiso contactarse con el administrador";
                    return BadRequest(resultDto);
                }

                var usuario = identity.Claims.FirstOrDefault(x => x.Type == "strUsuario");
                var result = await _applicationMovimiento.GetAll_Prioridad_RelacioArticuloMov(intIdRelacionArticuloPosicion);

                var jsonReponse = JsonConvert.SerializeObject(new { result });

                var content = new StringContent(jsonReponse, System.Text.Encoding.UTF8, "application/json");

                return new ContentResult
                {
                    Content = jsonReponse,
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
