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
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ViewBag.ReturnUrl = returnUrl;
            return View();

        }
        [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(Usuario model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            
            string passwordHash = CalcularHashMD5(model.Password);

                // Verificar si el usuario es "root"
                if (model.Correo == "root@gmail.com")
                {
                    // Autenticar como administrador
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "root@gmail.com"),
                new Claim(ClaimTypes.Role, "Administrador"),
                // Otras claims según sea necesario
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "Empleados"); // Cambiar a la acción adecuada para el administrador
                }
                // Verificar las credenciales del empleado
                var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Correo == model.Correo && e.Password == passwordHash);

            // Verificar las credenciales del cliente si no se encontró un empleado
            if (empleado == null)
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == model.Correo && c.Password == passwordHash);
            
                if (cliente != null)
                {
                    // Autenticación del cliente
                    // Crear las claims necesarias
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, cliente.Correo),
                        new Claim(ClaimTypes.Role, "Cliente"),
                        // Otras claims según sea necesario
                    };

                    // Crear el identity del cliente
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Iniciar sesión del cliente
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    // Redirigir al cliente al destino adecuado
                    return RedirectToAction("CatalogoRestaurante", "Home");
                }
            }
            else
            {
                // Autenticación del empleado
                // Crear las claims necesarias
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, empleado.Correo),
                    new Claim(ClaimTypes.Role, "Empleado"),
                    // Otras claims según sea necesario
                };

                // Crear el identity del empleado
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Iniciar sesión del empleado
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                // Redirigir al empleado al destino adecuado
                return RedirectToAction("Index", "Restaurantes");
            }
        
            ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
        }

       
        return View(model);
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
