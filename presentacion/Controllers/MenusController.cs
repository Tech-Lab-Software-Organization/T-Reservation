using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_Reservation.Models;
using T_RESERVATION.AccesoDatos;

namespace T_Reservation.Controllers
{
    [Authorize(Roles = "Administrador, Empleado")]

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
           
                var applicationDbContext = _context.Menu.Include(m => m.Restaurante);
                return View(await applicationDbContext.ToListAsync());
          
        }

        // GET: Menus/Details/5
        [Authorize(Roles = "Administrador, Empleado")]
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
        [Authorize(Roles = "Administrador, Empleado")]
        public IActionResult Create()
        {
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "IdRestaurante", "Nombre");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Empleado")]
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
        [Authorize(Roles = "Administrador, Empleado")]
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
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "IdRestaurante", "Nombre", menu.RestauranteId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador, Empleado")]
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
        [Authorize(Roles = "Administrador, Empleado")]
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
        [Authorize(Roles = "Administrador, Empleado")]
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
