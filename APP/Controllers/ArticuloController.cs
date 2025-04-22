using Api.Dtos.Articulo;
using Api.Dtos.Bodega;
using APP.Services.Articulo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly IArticulo _Iarticulo;
        private readonly string _strToken;
        public ArticuloController( IArticulo articulo, IHttpContextAccessor httpContextAccessor)
        {
            _strToken = httpContextAccessor.HttpContext.Session.GetString("strToken");

            _Iarticulo = articulo;
        }
        public async Task<IActionResult> Index()
        {
            var acciones = HttpContext.Session.GetString("AcionesRoles");
            var usuariGlobal = HttpContext.Session.GetString("UsuarioGlobal");
            var rolGlobal = HttpContext.Session.GetString("RolGlobal");
            var strTokenGlobal = HttpContext.Session.GetString("strToken");
            var Bodegas = HttpContext.Session.GetString("Bodegas");






            if (string.IsNullOrEmpty(strTokenGlobal))
            {
                HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
                return RedirectToAction("Index", "Home");
            }
            List<ArticuloDto> articulos = await _Iarticulo.ListadoArticulo(strTokenGlobal);
            var bodegJson = JsonConvert.DeserializeObject<List<BodegaDto>>(Bodegas);

            ViewBag.Bodegas = bodegJson;
            ViewBag.Articulo = articulos;
            return View();
        }

        [Authorize]
        public async Task<JsonResult> Get_Articulo_Posicion_NumParte(string strCodSucursal, string strCodArticulo)
        {
            List<DetailArticuloDto> ArticuloDetalle = await _Iarticulo.GetArticulo_Posicion_NumeroParte(_strToken, strCodSucursal, strCodArticulo);

            return Json(new { result = ArticuloDetalle });
        }
 
    }
}
