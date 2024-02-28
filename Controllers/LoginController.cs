using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T_Reservation.Models;

namespace T_Reservation.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public LoginController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
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

           
            var nombreUsuario = _contexto.ValidarLogin(model.Correo, model.Password);

            if (nombreUsuario != null)
            {
                //SI FURULA
                TempData["UsuarioCorreo"] = model.Correo;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // NO FURULA
                ModelState.AddModelError(string.Empty, "El correo o la contraseña son incorrectos.");
                return View(model);
            }
        }


    }
}
