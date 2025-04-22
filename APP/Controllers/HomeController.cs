using Api.Dtos.User;
using APP.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;
using APP.Services.User;

namespace APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser _Users;

        public HomeController(ILogger<HomeController> logger,IUser user)
        {
            _logger = logger;
            _Users = user;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginDto login)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Datos inválidos";
                return View("Index");
            }

            try
            {
                var users = await _Users.LoginRequest(login);

                if (users.IsSuccess && users.Item != null)
                {
                    string accionesJson = JsonConvert.SerializeObject(users.Item.Acciones);
                    string bodegas = JsonConvert.SerializeObject(users.Item.Bodegas);
                    HttpContext.Session.SetString("AcionesRoles", accionesJson);
                    HttpContext.Session.SetString("UsuarioGlobal", users.Item.strUsuario);
                    HttpContext.Session.SetString("RolGlobal", users.Item.Rol.intIdRol.ToString());
                    HttpContext.Session.SetString("strToken", users.Item.strToken);
                    HttpContext.Session.SetString("Bodegas", bodegas);

                    List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, login.strUsuario)
            };
                    ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new()
                    {
                        AllowRefresh = false,
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

                    return RedirectToAction("Index", "DashBoard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Credenciales inválidas";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error en la solicitud: {ex.Message}";
                return View("Index");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
