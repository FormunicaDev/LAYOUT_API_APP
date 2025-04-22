using Api.Dtos.Bodega;
using Api.Dtos.Common;
using Api.Dtos.Posicion;
using APP.Services.Posicion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP.Controllers
{
    public class PosicionController : Controller
    {
        private readonly IPosicion _IservicePosicion;
        private readonly string _strToken;
        private readonly List<BodegaDto> _bodegaDtoJson;
        private readonly string _baseurl;
     
        public PosicionController(IPosicion IservicePosicion, IHttpContextAccessor httpContextAccessor) {
            _strToken = httpContextAccessor.HttpContext.Session.GetString("strToken");
            var bodega= httpContextAccessor.HttpContext.Session.GetString("Bodegas");
            _bodegaDtoJson=JsonConvert.DeserializeObject<List<BodegaDto>>(bodega);
            this._IservicePosicion = IservicePosicion;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var Bodegas = HttpContext.Session.GetString("Bodegas");
         
            List<PosicionDto> posiciones = new List<PosicionDto>();
            posiciones=await _IservicePosicion.GetPosicion(_strToken, _bodegaDtoJson[0].strCodBodega.ToString());

           
            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.Posiciones=posiciones;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> FiltrarPosiciones(string codigoBodega)
        {
            var posiciones = await _IservicePosicion.GetPosicion(_strToken, codigoBodega);
           
            ViewBag.Bodegas = _bodegaDtoJson;
            ViewBag.Posiciones = posiciones;
            return View("Index");
        }
        [Authorize]
        public IActionResult GetImage(string imageName)
        {
            string imagePath = $@"\\10.10.0.8\ImgPosiciones\{imageName}";

            if (System.IO.File.Exists(imagePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(fileBytes, "image/png"); // Cambia el tipo MIME si la imagen no es PNG
            }

            return NotFound(); // Si no se encuentra la imagen
        }

        [Authorize]
        public async Task<ActionResult> Edit(string? id)
        {
            
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentException("El parámetro 'id' no puede ser nulo o vacío.");

                id = Uri.UnescapeDataString(id); // sirve cuando el parametro viene en el enlace
                
                int intId = Convert.ToInt32(EncryptionService.Descrypt(id));
                var posicion = await _IservicePosicion.SearchPosicion(_strToken, intId);
                TempData["IsSuccess"] = false;
                string imagePath = posicion.imgCodigoBarra; // Esto es el nombre de la imagen
                ViewBag.ImagePath = Path.GetFileName(imagePath);
                var tipoPosicion = await _IservicePosicion.Search_GetAll_TipoPosicion(_strToken, 0);
                ViewBag.TipoPosicion = tipoPosicion.Data;
                ViewBag.VarTipoPosicion=posicion.intIdTipoPosicion;
                ViewBag.Bodegas = _bodegaDtoJson;
                ViewBag.sucursal = posicion.strCodSucursal;
                PosicionDto posicionDto = new PosicionDto();
                posicionDto= posicion;

            return View(posicionDto);
           
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit([FromForm]PosicionDto posicionDto)
        {
            try
            {
                ResultDto<int> posicion = new ResultDto<int>();
                var usuariGlobal = HttpContext.Session.GetString("UsuarioGlobal");
                posicionDto.strUsuario = usuariGlobal.ToString();
                posicion =await _IservicePosicion.CreateOrUpdatePosicion(_strToken, posicionDto);
                var tipoPosicion = await _IservicePosicion.Search_GetAll_TipoPosicion(_strToken, 0);
                ViewBag.TipoPosicion = tipoPosicion.Data;
                ViewBag.VarTipoPosicion =posicionDto.intIdTipoPosicion;

                if (posicion.IsSuccess)
                {

                    List<PosicionDto> posiciones = new List<PosicionDto>();
                    posiciones = await _IservicePosicion.GetPosicion(_strToken, _bodegaDtoJson[0].strCodBodega.ToString());
                    TempData["IsSuccess"] = true;
                    TempData["Message"] = "Datos guardados correctamente.";
                    TempData["MessageType"] = "success";
                    string imagePath = posicionDto.imgCodigoBarra;
                    ViewBag.ImagePath = Path.GetFileName(imagePath);
                    ViewBag.Bodegas = _bodegaDtoJson;
                    ViewBag.sucursal = posicionDto.strCodSucursal;
                    ViewBag.TipoPosicion = tipoPosicion.Data;
                    ViewBag.VarTipoPosicion = posicionDto.intIdTipoPosicion;
                    return View("Edit");
                }
                else 
                {
                    TempData["Message"] = "Ocurrió un error al guardar los datos.";
                    TempData["MessageType"] = "error";
                    TempData["IsSuccess"]=true;
                    string imagePath = posicionDto.imgCodigoBarra; 
                    ViewBag.ImagePath = Path.GetFileName(imagePath);
                    ViewBag.Bodegas = _bodegaDtoJson;
                    ViewBag.sucursal = posicionDto.strCodSucursal;
                    
                    ViewBag.TipoPosicion = tipoPosicion.Data;
                    ViewBag.VarTipoPosicion = posicionDto.intIdTipoPosicion;
                    return View(posicionDto);
                }
               

            }
            catch (Exception ex) {
                TempData["Message"] = "Ocurrió un error inesperado."+ex.Message;
                TempData["MessageType"] = "error";
                return RedirectToAction("Editar");
            }
        }

        [Authorize]
        public async Task<ActionResult> Create()
        {
                var tipoPosicion= await _IservicePosicion.Search_GetAll_TipoPosicion(_strToken, 0);
                ViewBag.Bodegas = _bodegaDtoJson;
                ViewBag.TipoPosicion = tipoPosicion.Data; 
                PosicionDto posicionDto = new PosicionDto();
                return View(posicionDto);

        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] PosicionDto posicion)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Bodegas = _bodegaDtoJson;
                ResultDto<int> posiciones = new ResultDto<int>();
                var usuariGlobal = HttpContext.Session.GetString("UsuarioGlobal");
                posicion.strUsuario = usuariGlobal.ToString();
                posiciones = await _IservicePosicion.CreateOrUpdatePosicion(_strToken, posicion);
                ViewBag.Bodegas = _bodegaDtoJson;
                var tipoPosicion = await _IservicePosicion.Search_GetAll_TipoPosicion(_strToken, 0);
                ViewBag.TipoPosicion = tipoPosicion.Data;

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Datos guardados correctamente.";
                TempData["MessageType"] = "success";
                return View("Create");
               
            }

            ViewBag.Bodegas = _bodegaDtoJson;
            return View(posicion);
        }


        [Authorize]
        [HttpGet("/viewPdf")]
        public IActionResult ViewPdf(int id)
        {
            var pdfUrl = $"{_baseurl}/Posicion/DescargarPdf/{id}";

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(pdfUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var pdfBytes = response.Content.ReadAsByteArrayAsync().Result;
                    return File(pdfBytes, "application/pdf");
                }
            }
            return NotFound();
        }


    }
}
