using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_RESERVATION.EntidadesNegocio;
using T_RESERVATION.LogicaNegocio;

namespace T_Reservation.Controllers
{
    [Authorize(Roles = "Administrador, Empleado")]

    public class MenusController : Controller
    {
        private readonly MenuBL _menuBL;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenusController(MenuBL menuBL, IHttpContextAccessor httpContextAccessor)
        {
            _menuBL = menuBL;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            return View(await _menuBL.ObtenerTodo());
        }

        // GET: Menus/Details/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Details(int id)
        {

            var menu = await _menuBL.ObtenerId(new Menu { Id = id });
            if (menu == null)
            {
                return NotFound();
            }
            var restaurantes = await _menuBL.ObtenerRestaurante();
            ViewBag.restaurante = new SelectList(restaurantes, "RestauranteId", "Producto");
            return View(menu);
        }

        // GET: Menus/Create
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Create()
        {
            var restaurantes = await _menuBL.ObtenerRestaurante();

            // Asumiendo que "RestauranteId" es el identificador único del restaurante y "Nombre" es el nombre del restaurante
            ViewBag.Restaurante = new SelectList(restaurantes, "RestauranteId", "Nombre");

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
            await _menuBL.Crear(menu, imagen);
            return RedirectToAction(nameof(Index));
        }

        // GET: Menus/Edit/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Edit(int id)
        {
            var menu = await _menuBL.ObtenerId(new Menu { Id = id });
            if (menu == null)
            {
                return NotFound();
            }

            var menus = await _menuBL.ObtenerRestaurante();
            ViewBag.restaurante = new SelectList(menus, "RestauranteId", "Producto");

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


            try
            {
                await _menuBL.Modificar(menu, imagen);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(menu.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Menus/Delete/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Delete(int id)
        {
            var menus = await _menuBL.ObtenerId(new Menu { Id = id });
            if (menus == null)
            {
                return NotFound();
            }
            var menu = await _menuBL.ObtenerRestaurante();
            ViewBag.restaurante = new SelectList(menu, "RestauranteId", "Producto");
            return View(menus);
        }

        // POST: Menus/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[Authorize(Roles = "Administrador, Empleado")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Menu == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Menu'  is null.");
        //    }
        //    var menu = await _context.Menu.FindAsync(id);
        //    if (menu != null)
        //    {
        //        _context.Menu.Remove(menu);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        
        private bool MenuExists(int id)
        {
            return _menuBL.MenuExists(id);
        }
    }
}
