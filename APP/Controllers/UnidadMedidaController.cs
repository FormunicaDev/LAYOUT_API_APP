using Api.Dtos.Bodega;
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using Api.Dtos.UnidadMedida;
using APP.Services.Posicion;
using APP.Services.UnidadMedida;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP.Controllers
{
    public class UnidadMedidaController : Controller
    {
        private readonly string _strToken;
        private readonly List<BodegaDto> _bodegaDtoJson;
        private readonly IUnidadMedida _unidadMedida;
        public UnidadMedidaController(IHttpContextAccessor httpContextAccessor,IUnidadMedida unidadMedida) 
        {
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
            _unidadMedida = unidadMedida;
        }

        [HttpPost]
        public async Task<ActionResult> FiltrarUnidadMedida(string codigoBodega)
        {
            ResultDto<UnidadMedidaDto> unidadMedida = new ResultDto<UnidadMedidaDto>();
            unidadMedida=await _unidadMedida.GetUnidadMedida(_strToken, codigoBodega);


            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.UnidadMedida = unidadMedida.Data;
            return View("Index");
        }
        public async Task<IActionResult> Index()
        {
            if (_bodegaDtoJson == null || !_bodegaDtoJson.Any())
            {
                ViewBag.Bodegas = new List<BodegaDto>();
                ViewBag.UnidadMedida = new List<UnidadMedidaDto>(); 
                ViewBag.Error = "No hay bodegas disponibles.";
                return View();
            }

            var result = await _unidadMedida.GetUnidadMedida(_strToken, _bodegaDtoJson[0].strCodBodega);
            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.UnidadMedida = result.Data;
            return View();
        }


        public async Task<ActionResult> Create()
        {
            ViewBag.Bodegas = _bodegaDtoJson;
            UnidadMedidaDto unidad = new UnidadMedidaDto();
            return View(unidad);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UnidadMedidaDto unidadMedidaDto)
        {
            ViewBag.Bodegas = _bodegaDtoJson;
            ResultDto<int> unidadMedida = new ResultDto<int>();
            var usuariGlobal = HttpContext.Session.GetString("UsuarioGlobal");
            unidadMedidaDto.strUsuario = usuariGlobal;
            unidadMedidaDto.intIdUnidadMedida = 0;
            unidadMedida = await _unidadMedida.CreateOrUpdate_UnidadMedida(_strToken,unidadMedidaDto);
            TempData["IsSuccess"] = true;
            TempData["Message"] = unidadMedida.IsSuccess ? unidadMedida.Message:unidadMedida.MessageException;
            TempData["MessageType"] = unidadMedida.IsSuccess?"success":"error";
            return View("Create");
        }


        [HttpPost]
        public async Task<ActionResult> Edit(UnidadMedidaDto unidadMedidaDto)
        {
            ViewBag.Bodegas = _bodegaDtoJson;
            ResultDto<int> unidadMedida = new ResultDto<int>();
            var usuariGlobal = HttpContext.Session.GetString("UsuarioGlobal");
            unidadMedidaDto.strUsuario = usuariGlobal;
          
            unidadMedida = await _unidadMedida.CreateOrUpdate_UnidadMedida(_strToken, unidadMedidaDto);
            TempData["IsSuccess"] = true;
            TempData["Message"] = unidadMedida.IsSuccess ? unidadMedida.Message : unidadMedida.MessageException;
            TempData["MessageType"] = unidadMedida.IsSuccess ? "success" : "error";
            return View("Create");
        }



        public async Task<IActionResult> Edit(string ?id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("El parámetro 'id' no puede ser nulo o vacío.");

            id = Uri.UnescapeDataString(id); // sirve cuando el parametro viene en el enlace

            int intId = Convert.ToInt32(EncryptionService.Descrypt(id));
            ResultDto<UnidadMedidaDto> unidadMedida = await _unidadMedida.SearchUnidadMedida(_strToken, intId);
        

            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.sucursal = unidadMedida.Item.strCodSucursal;

            return View(unidadMedida.Item);
        }
    }
}
