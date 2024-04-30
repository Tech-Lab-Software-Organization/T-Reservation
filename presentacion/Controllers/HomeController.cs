using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using T_Reservation.Models;
using T_RESERVATION.AccesoDatos;

namespace T_Reservation.Controllers
{
    [Authorize(Roles = "Cliente,Administrador")]
    public class HomeController : Controller
    {
        

       
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> CatalogoMenu(string searchString)
        {
            var menu = from m in _context.Menu
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(m => m.Producto.Contains(searchString));
            }

            return View(await menu.ToListAsync());
            var applicationDbContext = _context.Menu.Include(m => m.Restaurante);
            return View(await applicationDbContext.ToListAsync());
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult IndexHome()
        {
            return View();
        }

        public async Task<IActionResult> VerMas(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .Include(m => m.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }


        public async Task<IActionResult> CatalogoRestaurante(string searchString)
        {
            var restaurantes = from r in _context.Restaurantes
                               select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                restaurantes = restaurantes.Where(r => r.Nombre.Contains(searchString));
            }

            

            return View(await restaurantes.ToListAsync());
            var applicationDbContext = _context.Restaurantes.Include(m => m.Menus);
            return View(await applicationDbContext.ToListAsync());
        }

        

        public async Task<IActionResult> VerMasR(int? id)
        {
            if (id == null || _context.Restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes
                .Include(m => m.Menus)
                .FirstOrDefaultAsync(m => m.IdRestaurante == id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return View(restaurante);
        }

        private bool restauranteExists(int id)
        {
            return _context.Restaurantes.Any(e => e.IdRestaurante == id);
        }


    }
}
