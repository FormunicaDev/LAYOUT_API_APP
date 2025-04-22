using Api.Abstractions.IApplications;
using Api.Applications.Bodega;
using Api.Dtos.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("Bodega")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private IApplicationBodega _applicationBodega;
        public BodegaController(IApplicationBodega applicationBodega)
        {
            this._applicationBodega = applicationBodega;
        }

        [Authorize]
        [HttpGet]
        [Route("ListadoBodegaUsuario")]
        public async Task<IActionResult> ListadoBodegaUsuario()
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
                var result = await _applicationBodega.ListadoBodegaUsuario(Convert.ToString(usuario.ToString()));
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
