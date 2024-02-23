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
    public class RestaurantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Restaurantes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Restaurantes.Include(r => r.Empleados);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Restaurantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes
                .Include(r => r.Empleados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return View(restaurante);
        }

        // GET: Restaurantes/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Correo");
            return View();
        }

        // POST: Restaurantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Direccion,EmpleadoId")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Correo", restaurante.EmpleadoId);
            return View(restaurante);
        }

        // GET: Restaurantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Correo", restaurante.EmpleadoId);
            return View(restaurante);
        }

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Direccion,EmpleadoId")] Restaurante restaurante)
        {
            if (id != restaurante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestauranteExists(restaurante.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Correo", restaurante.EmpleadoId);
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes
                .Include(r => r.Empleados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return View(restaurante);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restaurantes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Restaurantes'  is null.");
            }
            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante != null)
            {
                _context.Restaurantes.Remove(restaurante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestauranteExists(int id)
        {
          return _context.Restaurantes.Any(e => e.Id == id);
        }
    }
}
