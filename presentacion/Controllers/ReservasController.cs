using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_Reservation.Models;

namespace T_Reservation.Controllers
{
    [Authorize(Roles = "Administrador, Empleado,Cliente")]
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
       

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Reservas
        
        public async Task<IActionResult> Index()
        {
           

            var applicationDbContext = _context.Reservas.Include(r => r.Cliente).Include(r => r.Mesa).Include(r => r.Restaurante);
            return View(await applicationDbContext.ToListAsync());
          

        }
       


        // GET: Reservas/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Mesa)
                .Include(r => r.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        [Authorize(Roles = "Cliente")]
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Correo");
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Area");
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "IdRestaurante", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Cliente")]
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,RestauranteId,MesaId,CantidadPersonas,FechaInicio,FechaFin")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                bool reservaExistente = _context.Reservas.Any(r => r.FechaInicio.Date == reserva.FechaInicio.Date && r.RestauranteId == reserva.RestauranteId);

                if (reservaExistente)
                {
                    // Mostrar mensaje de error
                    ModelState.AddModelError(string.Empty, "Ya existe una reserva para el mismo día en este restaurante.");
                    // Recargar la vista con los datos ingresados por el usuario
                    ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Correo", reserva.ClienteId);
                    ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Area", reserva.MesaId);
                    ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "IdRestaurante", "Nombre", reserva.RestauranteId);
                    return View(reserva);
                }
               
            }
            _context.Add(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Correo", reserva.ClienteId);
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Area", reserva.MesaId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "IdRestaurante", "Nombre", reserva.RestauranteId);
            
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,RestauranteId,MesaId,CantidadPersonas,FechaInicio,FechaFin")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Correo", reserva.ClienteId);
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Area", reserva.MesaId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "IdRestaurante", "Nombre", reserva.RestauranteId);
            return View(reserva);
        }

        

        // GET: Reservas/Delete/5
        [Authorize(Roles = "Cliente, Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Mesa)
                .Include(r => r.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente, Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservas'  is null.");
            }
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
