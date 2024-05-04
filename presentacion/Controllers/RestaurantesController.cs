using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_RESERVATION.AccesoDatos;
using T_RESERVATION.EntidadesNegocio;
using T_RESERVATION.LogicaNegocio;

namespace T_Reservation.Controllers
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class RestaurantesController : Controller
    {
        private readonly RestauranteBL _context;

        public RestaurantesController(RestauranteBL context)
        {
            _context = context;
        }

        // GET: Restaurantes


        public async Task<IActionResult> Index()
        {
            var empleados = await _context.ObtenerEmpleado();
            ViewBag.empleado = new SelectList(empleados, "EmpladoId", "Nombre");
            return View(await _context.ObtenerTodo());
        }

        // GET: Restaurantes/Details/5

        public async Task<IActionResult> Details(int id)
        {
            var facturaVentum = await _context.ObtenerId(new Restaurante { IdRestaurante = id });
            if (facturaVentum == null)
            {
                return NotFound();
            }

            return View(facturaVentum);
            //if (id == null || _context.Restaurantes == null)
            //{
            //    return NotFound();
            //}

            //var restaurante = await _context.Restaurantes
            //     .Include(s => s.Mesas)
            //    .Include(r => r.Empleados)
            //    .FirstOrDefaultAsync(m => m.IdRestaurante == id);
            //if (restaurante == null)
            //{
            //    return NotFound();
            //}
            //ViewBag.Accion = "Details";
            //return View(restaurante);
        }

       
        public async Task<IActionResult> CreateAsync()
        {
            // Inicializar el modelo de restaurante
            var restaurante = new Restaurante();

            // Inicializar la lista de mesas del restaurante
            restaurante.Mesas = new List<Mesa>();
            restaurante.Mesas.Add(new Mesa
            {
                Numero = 1,
                Capacidad = 1
            });

            // Obtener la lista de empleados
            var empleados = await _context.ObtenerEmpleado();

            // Configurar datos necesarios en ViewBag o ViewData
            ViewBag.Accion = "Create";
            ViewBag.Empleados = new SelectList(empleados, "Id", "Nombre");

            // Pasar el modelo y cualquier otro dato necesario a la vista
            return View(restaurante);
        }


        // POST: Restaurantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> Create([Bind("IdRestaurante,Nombre,Descripcion,Direccion,EmpleadoId,Mesas")] Restaurante restaurante, IFormFile imagen)
        {

            await _context.Crear(restaurante, imagen);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<ActionResult> AgregarDetallesAsync([Bind("IdRestaurante,Nombre,Descripcion,Direccion,EmpleadoId,Mesas")] Restaurante restaurante, string accion)
        {


            var empleados = await _context.ObtenerEmpleado();


            ViewBag.emplado = new SelectList(empleados, "Id", "Nombre");

            ViewData["Imagen"] = restaurante.Imagen; // Mantener la imagen en la vista
            ViewBag.Accion = accion;

            // Devolver la vista con los datos actualizados
            return View(accion, restaurante);
        }

        public async Task<ActionResult> EliminarDetallesAsync([Bind("IdRestaurante,Nombre,Descripcion,Direccion,EmpleadoId,Mesas")] Restaurante restaurante, int index, string accion)
        {
            //// Eliminar la mesa seleccionada
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

            var empleados = await _context.ObtenerEmpleado();


            ViewBag.emplado = new SelectList(empleados, "Id", "Nombre");
            ViewData["Imagen"] = restaurante.Imagen; // Mantener la imagen en la vista

            // Devolver la vista con los datos actualizados
            return View(accion, restaurante);
        }

        // GET: Restaurantes/Edit/5
        //[Authorize(Roles = "Administrador, Empleado")]
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Restaurantes == null)
        //    {
        //        return NotFound();
        //    }

        //    var restaurante = await _context.Restaurantes
        //        .Include(s => s.Mesas)
        //        .Include(r => r.Empleados)
        //        .FirstOrDefaultAsync(m => m.IdRestaurante == id);
        //    if (restaurante == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre", restaurante.EmpleadoId);
        //    ViewBag.Accion = "Edit";
        //    return View(restaurante);
        //}

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador, Empleado")]
        //public async Task<IActionResult> Edit([Bind("IdRestaurante,Nombre,Descripcion,Direccion,EmpleadoId,Mesas")] T_RESERVATION.EntidadesNegocio.Restaurante restaurante, IFormFile imagen)
        //{
        //    try
        //    {
        //        if (imagen != null && imagen.Length > 0)
        //        {

        //            using (var memoryStream = new MemoryStream())
        //            {
        //                await imagen.CopyToAsync(memoryStream);
        //                restaurante.Imagen = memoryStream.ToArray();
        //            }
        //            _context.Update(restaurante);
        //            await _context.SaveChangesAsync();
        //        }

        //        else
        //        {
        //            var registroupdate = await _context.Restaurantes
        //                    .Include(s => s.Mesas)
        //                    .FirstAsync(s => s.IdRestaurante == restaurante.IdRestaurante);

        //            if (registroupdate?.Imagen?.Length > 0)
        //                restaurante.Imagen = registroupdate.Imagen;

        //            registroupdate.Nombre = restaurante.Nombre;
        //            registroupdate.Descripcion = restaurante.Descripcion;
        //            registroupdate.Direccion = restaurante.Direccion;
        //            // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
        //            var detNew = restaurante.Mesas.Where(s => s.Id == 0);
        //            foreach (var d in detNew)
        //            {
        //                registroupdate.Mesas.Add(d);
        //            }
        //            // Obtener todos los detalles que seran modificados y actualizar a la base de datos
        //            var detUpdate = restaurante.Mesas.Where(s => s.Id > 0);
        //            foreach (var d in detUpdate)
        //            {
        //                var det = registroupdate.Mesas.FirstOrDefault(s => s.Id == d.Id);
        //                det.Numero = d.Numero;
        //                det.Capacidad = d.Capacidad;
        //                det.Area = d.Area;
        //                det.Disponibilidad = d.Disponibilidad;
        //            }
        //            // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
        //            var delDet = restaurante.Mesas.Where(s => s.Id < 0).ToList();
        //            if (delDet != null && delDet.Count > 0)
        //            {
        //                foreach (var d in delDet)
        //                {
        //                    d.Id = d.Id * -1;
        //                    var det = registroupdate.Mesas.FirstOrDefault(s => s.Id == d.Id);
        //                    _context.Remove(det);

        //                }
        //            }
        //            _context.Update(registroupdate);
        //            await _context.SaveChangesAsync();
        //        }
        //    }


        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RestauranteExists(restaurante.IdRestaurante))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));

        //}

        // GET: Restaurantes/Delete/5
        [Authorize(Roles = "Empleado, Administrador")]
        public async Task<IActionResult> Delete(int id)
        {

            var facturaVentum = await _context.ObtenerId(new Restaurante { IdRestaurante = id });
            if (facturaVentum == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Delete";
            return View();
        }

        //POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador, Empleado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Eliminar(new Restaurante { IdRestaurante = id });
            return RedirectToAction(nameof(Index));
        }



        private bool RestauranteExists(int id)
        {
            return _context.RestauranteExists(id);
        }


        //    public ActionResult GraficoPorFecha()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    public async Task<ActionResult> GetInfoGraficoPorFecha()
        //    {
        //        var claimsPrincipal = HttpContext.User;

        //        // Access the "name" claim (containing email)
        //        var email = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        //        // Get the user ID from the database using email
        //        var usuario = await _context.Empleados.FirstOrDefaultAsync(u => u.Correo == email);

        //        // Check if user exists before proceeding
        //        if (usuario == null)
        //        {
        //            // Handle case where user is not found (e.g., return error)
        //            return BadRequest("Usuario no encontrado");
        //        }

        //        int empleadoId = usuario.Id; // Assuming EmpleadoId is the user ID

        //        var reservas = await _context.Reservas
        //            .Include(r => r.Restaurante)
        //            .Where(r => r.Restaurante.EmpleadoId == empleadoId)
        //            .ToListAsync();
        //        var objs = new List<object>();



        //        // Group reservations by restaurant name, reservation ID, and date, and count the number of reservations in each group
        //        var reservasPorFecha = reservas.GroupBy(r => r.FechaInicio.Date)
        //.Select(group => new
        //{
        //    fecha = group.Key,
        //    cantidad = group.Count(),
        //    // Si necesitas almacenar los nombres de los restaurantes o ids de reservas agrupados, puedes usar una lista o añadir propiedades adicionales:
        //    restaurantes = group.Select(r => r.Restaurante.Nombre).ToList(),
        //    reservasIds = group.Select(r => r.Id).ToList()
        //});


        //        foreach (var reserva in reservasPorFecha)
        //        {
        //            objs.Add(new
        //            {
        //                fecha = reserva.fecha.ToString("yyyy-MM-dd"),
        //                cantidad = reserva.cantidad,
        //                // Opcionalmente, incluye las propiedades para nombres o ids agrupados:
        //                restaurantes = reserva.restaurantes,
        //                reservasIds = reserva.reservasIds
        //            });
        //        }



        //        return Json(objs);
        //    }



    }
}
