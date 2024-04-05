using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using T_Reservation.Models;

namespace T_Reservation.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(ApplicationDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            _context = contexto;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginC(string ReturnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        //se recibe, se comparann datos
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginC([Bind("Correo,Password")] Cliente cliente, string ReturnUrl)
        {
            cliente.Password = CalcularHashMD5(cliente.Password);
            var usuarioAut = await _context.Clientes.FirstOrDefaultAsync(s => s.Correo == cliente.Correo && s.Password == cliente.Password);
            if (usuarioAut != null)
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, usuarioAut.Correo),
            new Claim(ClaimTypes.Role, usuarioAut.Rol),
            new Claim("Id", usuarioAut.Id.ToString())
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });

                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("CatalogoRestaurante", "Home");
            }
            else
            {
                ViewBag.Error = "Credenciales Incorrectas";
                ViewBag.ReturnUrl = ReturnUrl;
                return View(cliente);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginE(string ReturnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        //se recibe, se comparann datos
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginE([Bind("Correo,Password")] Empleado empleado, string ReturnUrl)
        {
            empleado.Password = CalcularHashMD5(empleado.Password);
            var usuarioAut = await _context.Empleados.FirstOrDefaultAsync(s => s.Correo == empleado.Correo && s.Password == empleado.Password);
            if (usuarioAut != null)
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, usuarioAut.Correo),
            new Claim(ClaimTypes.Role, usuarioAut.Rol),
            new Claim("Id", usuarioAut.Id.ToString())
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });

                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Index", "Restaurantes");
            }
            else
            {
                ViewBag.Error = "Credenciales Incorrectas";
                ViewBag.ReturnUrl = ReturnUrl;
                return View(empleado);
            }
        }
       
        private string CalcularHashMD5(string texto)
        {
            using (MD5 md5 = MD5.Create())
            {
                //Convierte la cadena de texto a bytes
                byte[] inputbytes = Encoding.UTF8.GetBytes(texto);

                //Calcula el hash MD5 de los bytes
                byte[] HashBytes = md5.ComputeHash(inputbytes);

                //convierte el hash a una cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < HashBytes.Length; i++)
                {
                    sb.Append(HashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}
