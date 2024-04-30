using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_Reservation.Models;
using T_RESERVATION.AccesoDatos;

namespace T_Reservation.Controllers
{
    [Authorize(Roles = "Administrador, Empleado")]
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
                 .Include(s => s.Mesas)
                .Include(r => r.Empleados)
                .FirstOrDefaultAsync(m => m.IdRestaurante == id);
            if (restaurante == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Details";
            return View(restaurante);
        }

        // GET: Restaurantes/Create
        [Authorize(Roles = "Administrador,Empleado")]
        public IActionResult Create()
        {
            var restaurant = new Restaurante();


            restaurant.Mesas = new List<Mesa>();
            restaurant.Mesas.Add(new Mesa
            {
                Numero = 1,
                Capacidad = 1,
            });

            ViewBag.Accion = "Create";
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre");
            return View(restaurant);
        }

        // POST: Restaurantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> Create([Bind("IdRestaurante,Nombre,Descripcion,Direccion,EmpleadoId,Mesas")] Restaurante restaurante, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    restaurante.Imagen = memoryStream.ToArray();

                }
            }
            //// Asignar RestauranteId a las mesas
            //foreach (var mesa in restaurante.Mesas)
            //{
            //    mesa.IdRestaurante = restaurante.Id;
            //}
            _context.Add(restaurante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult AgregarDetalles([Bind("IdRestaurante,Nombre,Descripcion,Direccion,EmpleadoId,Mesas")] Restaurante restaurante, string accion)
        {
            // Agregar una nueva mesa al restaurante
            restaurante.Mesas.Add(new Mesa());

            // Mantener la imagen y el empleado seleccionado en la vista

            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre", restaurante.EmpleadoId);
            ViewData["Imagen"] = restaurante.Imagen; // Mantener la imagen en la vista
            ViewBag.Accion = accion;

            // Devolver la vista con los datos actualizados
            return View(accion, restaurante);
        }

        public ActionResult EliminarDetalles([Bind("IdRestaurante,Nombre,Descripcion,Direccion,EmpleadoId,Mesas")] Restaurante restaurante, int index, string accion)
        {
            // Eliminar la mesa seleccionada
            var det = restaurante.Mesas[index];
            if (accion == "Edit" && det.Id > 0)
            {
                det.Id = det.Id * -1;
            }
            else
            {
                restaurante.Mesas.RemoveAt(index);
            }


            ViewBag.Accion = accion;
            // Mantener la imagen y el empleado seleccionado en la vista

            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre", restaurante.EmpleadoId);
            ViewData["Imagen"] = restaurante.Imagen; // Mantener la imagen en la vista
  
            // Devolver la vista con los datos actualizados
            return View(accion, restaurante);
        }

        // GET: Restaurantes/Edit/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes
                .Include(s => s.Mesas)
                .Include(r => r.Empleados)
                .FirstOrDefaultAsync(m => m.IdRestaurante == id);
            if (restaurante == null)
            {
                return NotFound();
            }
           
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre", restaurante.EmpleadoId);
            ViewBag.Accion = "Edit";
            return View(restaurante);
        }

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Edit([Bind("IdRestaurante,Nombre,Descripcion,Direccion,EmpleadoId,Mesas")] Restaurante restaurante, IFormFile imagen)
        {
            try
            {
                if (imagen != null && imagen.Length > 0)
                {

                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        restaurante.Imagen = memoryStream.ToArray();
                    }
                    _context.Update(restaurante);
                    await _context.SaveChangesAsync();
                }

                else
                {
                    var registroupdate = await _context.Restaurantes
                            .Include(s => s.Mesas)
                            .FirstAsync(s => s.IdRestaurante == restaurante.IdRestaurante);

                    if (registroupdate?.Imagen?.Length > 0)
                        restaurante.Imagen = registroupdate.Imagen;

                    registroupdate.Nombre = restaurante.Nombre;
                    registroupdate.Descripcion = restaurante.Descripcion;
                    registroupdate.Direccion = restaurante.Direccion;
                    // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                    var detNew = restaurante.Mesas.Where(s => s.Id == 0);
                    foreach (var d in detNew)
                    {
                        registroupdate.Mesas.Add(d);
                    }
                    // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                    var detUpdate = restaurante.Mesas.Where(s => s.Id > 0);
                    foreach (var d in detUpdate)
                    {
                        var det = registroupdate.Mesas.FirstOrDefault(s => s.Id == d.Id);
                        det.Numero = d.Numero;
                        det.Capacidad = d.Capacidad;
                        det.Area = d.Area;
                        det.Disponibilidad = d.Disponibilidad;
                    }
                    // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                    var delDet = restaurante.Mesas.Where(s => s.Id < 0).ToList();
                    if (delDet != null && delDet.Count > 0)
                    {
                        foreach (var d in delDet)
                        {
                            d.Id = d.Id * -1;
                            var det = registroupdate.Mesas.FirstOrDefault(s => s.Id == d.Id);
                            _context.Remove(det);

                        }
                    }
                    _context.Update(registroupdate);
                    await _context.SaveChangesAsync();
                }
            }


            catch (DbUpdateConcurrencyException)
            {
                if (!RestauranteExists(restaurante.IdRestaurante))
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

        // GET: Restaurantes/Delete/5
        [Authorize(Roles = "Empleado, Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes
                .Include(s => s.Mesas)
                .Include(r => r.Empleados)
                .FirstOrDefaultAsync(m => m.IdRestaurante == id);
            if (restaurante == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Delete";
            return View(restaurante);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador, Empleado")]
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
          return _context.Restaurantes.Any(e => e.IdRestaurante == id);
        }


        public ActionResult GraficoPorFecha()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetInfoGraficoPorFecha()
        {
            var claimsPrincipal = HttpContext.User;

            // Access the "name" claim (containing email)
            var email = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // Get the user ID from the database using email
            var usuario = await _context.Empleados.FirstOrDefaultAsync(u => u.Correo == email);

            // Check if user exists before proceeding
            if (usuario == null)
            {
                // Handle case where user is not found (e.g., return error)
                return BadRequest("Usuario no encontrado");
            }

            int empleadoId = usuario.Id; // Assuming EmpleadoId is the user ID

            var reservas = await _context.Reservas
                .Include(r => r.Restaurante)
                .Where(r => r.Restaurante.EmpleadoId == empleadoId)
                .ToListAsync();
            var objs = new List<object>();



            // Group reservations by restaurant name, reservation ID, and date, and count the number of reservations in each group
            var reservasPorFecha = reservas.GroupBy(r => r.FechaInicio.Date)
    .Select(group => new
    {
        fecha = group.Key,
        cantidad = group.Count(),
        // Si necesitas almacenar los nombres de los restaurantes o ids de reservas agrupados, puedes usar una lista o añadir propiedades adicionales:
        restaurantes = group.Select(r => r.Restaurante.Nombre).ToList(),
        reservasIds = group.Select(r => r.Id).ToList()
    });


            foreach (var reserva in reservasPorFecha)
            {
                objs.Add(new
                {
                    fecha = reserva.fecha.ToString("yyyy-MM-dd"),
                    cantidad = reserva.cantidad,
                    // Opcionalmente, incluye las propiedades para nombres o ids agrupados:
                    restaurantes = reserva.restaurantes,
                    reservasIds = reserva.reservasIds
                });
            }



            return Json(objs);
        }



    }
}
