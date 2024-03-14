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
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservasController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var usuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioCorreo");
            var usuarioRol = _httpContextAccessor.HttpContext.Session.GetString("UsuarioRol");


            

            if (usuarioRol == "Administrador")
            {
            var applicationDbContext = _context.Reservas.Include(r => r.Cliente).Include(r => r.Mesa).Include(r => r.Restaurante);
            return View(await applicationDbContext.ToListAsync());
                 }

              else if (usuarioRol == "Cliente")
             {
                int usuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId") ?? default(int);

                // Pasar la ID del usuario a la vista
                ViewBag.UsuarioId = usuarioId;
                
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == usuarioId);

                if (cliente != null)
                {

                    var reservas = await _context.Reservas.Where(r => r.ClienteId == usuarioId).Include(r => r.Cliente).Include(r => r.Mesa).Include(r => r.Restaurante).ToListAsync();
                    return View(reservas);
                }
                
                
            }

            else if (usuarioRol == "Empleado" || usuario == "Dueño")
            {
                int usuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId") ?? default(int);
                var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Id == usuarioId);

                ViewBag.UsuarioId = usuarioId;

                if (empleado != null)
                {
                    var reservas = await _context.Reservas.Where(r => r.Restaurante.EmpleadoId == usuarioId).Include(r => r.Cliente).Include(r => r.Mesa).Include(r => r.Restaurante).ToListAsync();
                    return View(reservas);
                }
                
            }

            return RedirectToAction("Index", "Login");
        
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
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Correo");
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Area");
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,RestauranteId,MesaId,CantidadPersonas,FechaInicio,FechaFin")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Correo", reserva.ClienteId);
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Area", reserva.MesaId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre", reserva.RestauranteId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
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
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre", reserva.RestauranteId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre", reserva.RestauranteId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
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
