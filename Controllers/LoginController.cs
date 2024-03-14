using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T_Reservation.Models;

namespace T_Reservation.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _contexto;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(ApplicationDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }


            var cliente = _contexto.Clientes.FirstOrDefault(c => c.Correo == model.Correo && c.Passaword == model.Password);
            var empleado = _contexto.Empleados.FirstOrDefault(e => e.Correo == model.Correo && e.Password == model.Password);

            if (cliente != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UsuarioCorreo", model.Correo);
                _httpContextAccessor.HttpContext.Session.SetString("UsuarioRol", "Cliente");
                _httpContextAccessor.HttpContext.Session.SetInt32("UsuarioId", cliente.Id);
                return RedirectToAction("CatalogoRestaurante", "Home");
                
            }
            else if (empleado != null)
            {

                _httpContextAccessor.HttpContext.Session.SetString("UsuarioCorreo", model.Correo);
                _httpContextAccessor.HttpContext.Session.SetString("UsuarioRol", empleado.Rol);
                _httpContextAccessor.HttpContext.Session.SetInt32("UsuarioId", empleado.Id);
                return RedirectToAction("Index", "Restaurantes");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "El correo o la contraseña son incorrectos.");
                return View(model);
            }
        }

       


    }
}
