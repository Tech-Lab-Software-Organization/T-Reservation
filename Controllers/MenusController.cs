using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_Reservation.Models;

namespace T_Reservation.Controllers
{
    public class MenusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenusController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            var usuarioRol = _httpContextAccessor.HttpContext.Session.GetString("UsuarioRol");

            if (usuarioRol == "Administrador" || usuarioRol == "Cliente")
            {

                var applicationDbContext = _context.Menu.Include(m => m.Restaurante);
                return View(await applicationDbContext.ToListAsync());
            }


            else if (usuarioRol == "Empleado")
            {
                int usuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId") ?? default(int);
                var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Id == usuarioId);

                
                 var menus = await _context.Menu
                .Include(m => m.Restaurante) 
                .Where(m => m.Restaurante.EmpleadoId == usuarioId)
                .ToListAsync();

               
                foreach (var menu in menus)
                {
                    Console.WriteLine($"Menu ID: {menu.Id}, Restaurant Name: {menu.Restaurante.Nombre}");
                }

                return View(menus); 

            }

            
            return RedirectToAction("Index", "Login");
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Menus/Create
        public IActionResult Create()
        {
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Producto,Descripcion,NotaEspecial,Precio,RestauranteId")] Menu menu, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    menu.Imagen = memoryStream.ToArray();

                }
            }
            _context.Add(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre", menu.RestauranteId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Producto,Descripcion,NotaEspecial,Precio,RestauranteId")] Menu menu, IFormFile imagen)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (imagen != null && imagen.Length > 0)
            {
                
                 using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    menu.Imagen = memoryStream.ToArray();
                }
                _context.Update(menu);
                await _context.SaveChangesAsync();
            }

            else
            {
                var registroFind = await _context.Menu.FirstOrDefaultAsync(s => s.Id == menu.Id);

                if (registroFind?.Imagen?.Length > 0)
                    menu.Imagen = registroFind.Imagen;

                registroFind.Producto = menu.Producto;
                registroFind.Descripcion = menu.Descripcion;
                registroFind.NotaEspecial = menu.NotaEspecial;
                registroFind.Precio = menu.Precio;
                registroFind.RestauranteId = menu.RestauranteId;

                _context.Update(registroFind);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));


           
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menu == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Menu'  is null.");
            }
            var menu = await _context.Menu.FindAsync(id);
            if (menu != null)
            {
                _context.Menu.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool MenuExists(int id)
        {
          return _context.Menu.Any(e => e.Id == id);
        }
    }
}
