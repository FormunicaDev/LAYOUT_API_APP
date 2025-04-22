using Api.Dtos.Articulo;
using Api.Dtos.Bodega;
using Api.Dtos.Common;
using Api.Dtos.Movimientos;
using Api.Dtos.Posicion;
using Api.Dtos.Prioridad;
using Api.Dtos.RelacionArticulo;
using Api.Dtos.UnidadMedida;
using APP.Services.Articulo;
using APP.Services.Movimiento;
using APP.Services.Posicion;
using APP.Services.Prioridad;
using APP.Services.RelacionArticulo;
using APP.Services.UnidadMedida;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace APP.Controllers
{
    public class RelacionArticuloController : Controller
    {
        private readonly IRelacionArticulo _irelacionArticulo;
        private readonly IPosicion _posicion;
        private readonly IUnidadMedida _unidadMedida;
        private readonly IArticulo _articulo;
        private readonly IPrioridad _prioridad;
        private readonly string _strToken;
        private readonly List<BodegaDto> _bodegaDtoJson;
        private readonly IMovimientos _movimientos;
        private readonly string _usuariGlobal;
        public RelacionArticuloController(IHttpContextAccessor httpContextAccessor,IRelacionArticulo relacionArticulo,IPosicion posicion,IUnidadMedida unidadMedida,IArticulo articulo,IPrioridad prioridad,IMovimientos movimientos)
        {
             _usuariGlobal = httpContextAccessor.HttpContext.Session.GetString("UsuarioGlobal");
            _strToken = httpContextAccessor.HttpContext.Session.GetString("strToken");
            var bodega = httpContextAccessor.HttpContext.Session.GetString("Bodegas");

            if (!string.IsNullOrEmpty(bodega))
            {
                _bodegaDtoJson = JsonConvert.DeserializeObject<List<BodegaDto>>(bodega);
            }
            else
            {
                _bodegaDtoJson = new List<BodegaDto>(); // Inicializa vacío si no hay datos
            }

          
            _bodegaDtoJson = JsonConvert.DeserializeObject<List<BodegaDto>>(bodega);
            _irelacionArticulo = relacionArticulo;
            _posicion= posicion;
            _unidadMedida= unidadMedida;
            _articulo= articulo;
            _prioridad =prioridad;
            _movimientos = movimientos;

        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Bodegas = _bodegaDtoJson;
            var relacionArticulo = await _irelacionArticulo.GetRelacionArticulo(_strToken, _bodegaDtoJson.FirstOrDefault().strCodBodega);
            ViewBag.relacionArticulo = relacionArticulo;
            ViewBag.UnidadMedida = new List<UnidadMedida>();
            List<ArticuloDto> articulos = await _articulo.ListadoArticulo(_strToken);
            ViewBag.Articulo = articulos;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> FiltrarRelacionArticulo(string codigoBodega)
        {
            TempData["MostrarMensaje"] = false;
            var relacionArticulo = await _irelacionArticulo.GetRelacionArticulo(_strToken, codigoBodega);
            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.relacionArticulo = relacionArticulo.Data;
            ViewBag.UnidadMedida = new List<UnidadMedida>();
            return View("Index");
        }


        public async Task<ActionResult> Create()
        {
            TempData["MostrarMensaje"] = false;
            List<ArticuloDto> articulos = await _articulo.ListadoArticulo(_strToken);
            List<PrioridadDto> prioridad = await _prioridad.GetPrioridad(_strToken);
            ViewBag.Articulo = articulos;
            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.Posiciones = new List<PosicionDto>();
            ViewBag.UnidadMedida = new List<UnidadMedida>();
            ViewBag.Prioridad=prioridad;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RelacionArticuloDto resul)
        {
            decimal stock = resul.decUnidadesFisica;
            TempData["MostrarMensaje"] = false;
            List<ArticuloDto> articulos = await _articulo.ListadoArticulo(_strToken);
            ViewBag.Articulo = articulos;
            var posiciones = new List<PosicionDto>();
            ResultDto<UnidadMedidaDto> UnidadMedida=new ResultDto<UnidadMedidaDto>();
            var Prioridad=new List<PrioridadDto>();

            if (!string.IsNullOrEmpty(resul.strCodSucursal))
            {
                posiciones =  await _posicion.GetPosicion(_strToken, resul.strCodSucursal);
                UnidadMedida = await _unidadMedida.GetUnidadMedida(_strToken, resul.strCodSucursal);
                Prioridad = await _prioridad.GetPrioridad(_strToken);
                
            }
            ViewBag.Prioridad = Prioridad.ToList();
            ViewBag.UnidadMedida= UnidadMedida.Data.ToList();
            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.Posiciones = posiciones.ToList();
            ViewBag.BodegaSeleccionada = resul.strCodSucursal;
            ViewBag.PrioridadSeleccionada=resul.intIdPrioridad;
            ViewBag.ArticuloSeleccionada = resul.strCodArticulo;
            resul.decUnidadesFisica = 0;

            var resposeCreate = new ResultDto<int>();
            var responseHeaderMov =new ResultDto<int>();
            var resposeDetailMov = new ResultDto<int>();
            var resquestHeader=new MovimientoRequestDto();
            var requestDetail = new MovimientoDetailRequestDto();
            if (
                    !string.IsNullOrEmpty(resul.intIdUnidadMedida.ToString()) &&
                    (resul.intIdPrioridad != 0) &&
                    !string.IsNullOrEmpty(resul.intIdPrioridad.ToString()) &&
                    !string.IsNullOrEmpty(resul.strCodArticulo) &&
                    !string.IsNullOrEmpty(resul.strCodSucursal)
                    )
                { 
                    resposeCreate = await _irelacionArticulo.CreateOrUpdateRelacionArticulo(_strToken,resul);
                    resquestHeader.intIdTipoMovimiento = 7;
                    resquestHeader.strNumeroDocumento = "";
                    resquestHeader.strDescripcion = "STOCK INICIAL";
                    resquestHeader.strUsuario = _usuariGlobal;
                    responseHeaderMov = await _movimientos.Insert_MovimientoHeader(_strToken, resquestHeader);
                    requestDetail.strUsuario = _usuariGlobal;
                    requestDetail.intIdMovimientoArticulo = responseHeaderMov.Item;
                    requestDetail.decCantidad = stock;
                    requestDetail.intIdRelacionArticuloPosicion=resposeCreate.Item;
                    resposeDetailMov=await _movimientos.Insert_MovimientoDetail(_strToken, requestDetail);

                    TempData["MostrarMensaje"] = true;
                    TempData["IsSuccess"] = resposeCreate.IsSuccess ? true:false;
                    TempData["Message"] = resposeCreate.IsSuccess ? resposeCreate.Message : resposeCreate.Message.ToUpper();
                    TempData["MessageType"] = resposeCreate.IsSuccess ? "success" : "error";
                    TempData["MessegeSuccess"] = "Se Resgistro Correctamente";
                 
                
                 }
            return View();
        }


    
        public async Task<IActionResult> Edit(string id)
        {

            List<ArticuloDto> articulos = await _articulo.ListadoArticulo(_strToken);
            ViewBag.Articulo = articulos;
            var posiciones = new List<PosicionDto>();
            ResultDto<UnidadMedidaDto> UnidadMedida = new ResultDto<UnidadMedidaDto>();
            var Prioridad = new List<PrioridadDto>();

            id = Uri.UnescapeDataString(id); // sirve cuando el parametro viene en el enlace

            int intId = Convert.ToInt32(EncryptionService.Descrypt(id));
            ResultDto<RelacionArticuloDto> relacion = await _irelacionArticulo.SearchRelacionArticulo(_strToken, intId,"*");

            posiciones = await _posicion.GetPosicion(_strToken, relacion.Item.strCodSucursal);
            UnidadMedida = await _unidadMedida.GetUnidadMedida(_strToken, relacion.Item.strCodSucursal);
            Prioridad = await _prioridad.GetPrioridad(_strToken);

            ViewBag.Prioridad = Prioridad.ToList();
            ViewBag.UnidadMedida = UnidadMedida.Data.ToList();
            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.Posiciones = posiciones.ToList();
            
            return View(relacion.Item);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RelacionArticuloDto relacionArticuloDto)
        {
            try
            {
                List<ArticuloDto> articulos = await _articulo.ListadoArticulo(_strToken);
                ViewBag.Articulo = articulos;

                var posiciones = new List<PosicionDto>();
                ResultDto<UnidadMedidaDto> UnidadMedida = new ResultDto<UnidadMedidaDto>();
                var Prioridad = await _prioridad.GetPrioridad(_strToken);

                ViewBag.Bodegas = _bodegaDtoJson;
                ViewBag.Posiciones = posiciones.ToList();
                ViewBag.Prioridad = Prioridad.ToList();
                ViewBag.UnidadMedida = UnidadMedida.Data?.ToList() ?? new List<UnidadMedidaDto>();

                var resposeCreate = await _irelacionArticulo.CreateOrUpdateRelacionArticulo(_strToken, relacionArticuloDto);

                TempData["MostrarMensaje"] = true;
                TempData["IsSuccess"] = resposeCreate.IsSuccess;
                TempData["Message"] = resposeCreate.IsSuccess ? resposeCreate.Message : resposeCreate.MessageException;
                TempData["MessegeSuccess"] = "Se Registró Correctamente";

                return View();
            }
            catch (Exception ex)
            {
                TempData["MostrarMensaje"] = true;
                TempData["IsSuccess"] = false;
                TempData["Message"] = $"Error: {ex.Message}";
                return View();
            }
        }


        public async Task<JsonResult> ValueUnidadMedida(int intIdUnidadMedida)
        {
            var rep = await _unidadMedida.SearchUnidadMedida(_strToken, intIdUnidadMedida);
            return Json(new { decCantidadUnidad=rep.Item.decCantidadUnidad });
        }
    }
}
