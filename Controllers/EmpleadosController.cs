using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using T_Reservation.Models;

namespace T_Reservation.Controllers
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empleados

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Empleados.ToListAsync());
        }

        // GET: Empleados/Details/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        [Authorize(Roles = "Administrador")]

        public IActionResult Create()
        {
            Empleado empleado = new Empleado();
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Dui,FechaNacimiento,Direccion,Correo,Telefono,Password,RestauranteId")] Empleado empleado)
        {
            empleado.Password = CalcularHashMD5(empleado.Password);
            _context.Add(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Restaurantes");
        }

        // GET: Empleados/Edit/5
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Dui,FechaNacimiento,Direccion,Correo,Telefono,RestauranteId")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    var originalEmpledo = await _context.Empleados.FindAsync(id);

                    // Copiar la contraseña original al cliente que se va a actualizar
                    empleado.Password = originalEmpledo.Password;

                    // Actualizar el resto de las propiedades del cliente
                    _context.Entry(originalEmpledo).CurrentValues.SetValues(empleado);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return _context.Empleados.Any(e => e.Id == id);
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

        //public List<string> GetBotonesPorRol(string rol)
        //{
        //    switch (rol)
        //    {
        //        case "Administrador":
        //            return new List<string>() { "Crear", "Editar", "Eliminar" };
        //        case "Empleado":
        //            return new List<string>() { "Ver", "Editar" };
        //        case "Dueño":
        //            return new List<string>() { "Ver", "Crear" };
        //        default:
        //            return new List<string>();
        //    }
        //}
    }
}
