using Api.Dtos.Articulo;
using Api.Dtos.Bodega;
using Api.Dtos.Movimientos;
using Api.Dtos.Posicion;
using APP.Services.Articulo;
using APP.Services.Movimiento;
using APP.Services.Posicion;
using APP.Services.Prioridad;
using APP.Services.RelacionArticulo;
using APP.Services.UnidadMedida;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace APP.Controllers
{
    public class OperacionesController : Controller
    {
        private readonly IArticulo _IarticuloServices;
        private readonly IRelacionArticulo _relacionArticulo; 
        private readonly List<BodegaDto> _bodegaDtoJson;
        private readonly string _strToken;
        private readonly IMovimientos _movimientos;
        private readonly string _usuarioGlobal;
        public OperacionesController(IHttpContextAccessor httpContextAccessor, IRelacionArticulo relacionArticulo, IArticulo articulo,IMovimientos movimientos)
        {
            _usuarioGlobal = httpContextAccessor.HttpContext.Session.GetString("UsuarioGlobal");
            _strToken = httpContextAccessor.HttpContext.Session.GetString("strToken");
            var bodega = httpContextAccessor.HttpContext.Session.GetString("Bodegas");
            _bodegaDtoJson = JsonConvert.DeserializeObject<List<BodegaDto>>(bodega);
            _IarticuloServices = articulo;
            _relacionArticulo = relacionArticulo;
            _movimientos = movimientos;
        }

        public async Task<ActionResult> Movimientos()
        {
            var tipoMovimiento = await _movimientos.GetAll_Search_TipoMovimiento(_strToken, 0);
            ViewBag.ListTipoMovimiento = tipoMovimiento.Data;
            ViewBag.ListBodegas = _bodegaDtoJson;
            ViewBag.strUser=_usuarioGlobal.ToString();
            var Movimientos = await _movimientos.GetAll_Search_Mov_HeaderxTipoMov(_strToken, 0, _bodegaDtoJson[0].strCodBodega, _usuarioGlobal);
            ViewBag.ListMovimientoHeader = Movimientos.Data.ToList();
            return View();
        }



        public async  Task<ActionResult> CreateMovimiento() {
            ViewBag.strUser = _usuarioGlobal.ToString();
            var tipoMovimiento = await _movimientos.GetAll_Search_TipoMovimiento(_strToken, 0);
            ViewBag.ListTipoMovimiento = tipoMovimiento.Data;
            ViewBag.ListBodegas = _bodegaDtoJson;
            ViewBag.Posiciones = new List<PosicionDto>();
            ViewBag.UnidadMedida = new List<UnidadMedida>();

            return View();
        }

        public async Task<JsonResult> TempInsertarMovimiento([FromBody] tempMovimientoRequestDto tempMovimientoRequestDto)
        {
          
            var result = await _movimientos.TempInsertMovimiento(_strToken, tempMovimientoRequestDto);
            try
            {
                if (result.IsSuccess && result.Item != Guid.Empty)
                {
                    return Json(new
                    {
                        isSuccess = true,
                        item = result.Item
                    });

                }
                return Json(new
                {
                    isSuccess = false,
                    item = ""
                });

            }
           catch(Exception ex)
            {
                return Json(new
                {
                    isSuccess = false,
                    item = result.Item
                }); 
            }
        }
           

        public async Task<JsonResult> TempInsertarMovimientoDetalle([FromBody] tempMovimientoDetalleRequestDto tempMovimientoDetalleRequestDto)
        {
            var result = await _movimientos.TempInsertMovimientoDetalle(_strToken, tempMovimientoDetalleRequestDto);
            if (result.IsSuccess && result.Item != Guid.Empty)
            {
                return Json(new
                {
                    isSuccess = true,
                    item = result.Item
                });

            }
            return Json(new
            {
                isSuccess = false,
                item = ""
            });
        }

        public async Task<JsonResult> BuscarArticulo(string strBusqueda)
        {

            var result = await _IarticuloServices.SearchArticulo(_strToken, strBusqueda);
            if (result == null || !result.Any())
            {
                return Json(new { success = false, message = "No se encontraron artículos", data = new List<object>() });
            }

            var formattedResult = result.Select(a => new
            {
                id = a.strCodArticulo,  // ID debe coincidir con el JS
                text = a.DescripcionCompleta     // Texto a mostrar en Select2
            }).ToList();

            return Json(new { success = true, data= formattedResult });
        }

        public async Task<JsonResult> BuscarPosicion_RelacionArticulo(string strCodSucursal, string strCodArticulo)
        {
            var resultado = await _movimientos.GetAll_Posicion_RelacionArticulo(_strToken, strCodSucursal, strCodArticulo);
            return Json(new { sucess = true, data = resultado });
        }

        public async Task<JsonResult> BuscarUnidadMedida_RelacionArticulo(string intIdPosicion, string strCodSucursal,string strCodArticulo)
        {
            int idposicion=Convert.ToInt32(intIdPosicion);
            var resultado = await _movimientos.GetAll_UnidadMedida_RelacionArticulo(_strToken, idposicion, strCodSucursal,strCodArticulo);
            return Json(new { sucess = true, data = resultado });
        }

        public async Task<JsonResult> BuscarPrioridad(int intIdRelacionArticuloPosicion)
        {
            var resultado = await _movimientos.GetAll_Prioridades_Mov(_strToken, intIdRelacionArticuloPosicion);
            return Json(new { success = true, data = resultado });
        }

    }
}
