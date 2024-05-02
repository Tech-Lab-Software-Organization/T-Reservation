using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using T_RESERVATION.LogicaNegocio;
using T_RESERVATION.EntidadesNegocio;
using T_RESERVATION.AccesoDatos;
using T_Reservation.Models;

namespace T_Reservation.Controllers
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class EmpleadosController : Controller
    {
        private readonly EmpleadosBL _context;

        public EmpleadosController(EmpleadosBL context)
        {
            _context = context;
        }

        // GET: Empleados

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.ObtenerTodo());
        }

        // GET: Empleados/Details/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Details(int id)
        {
            var clientes = await _context.ObtenerId(new Empleado { Id = id });
            return View(clientes);
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
        public async Task<IActionResult> Create(Empleado empleado)
        {
            try
            {
                await _context.Crear(empleado);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Empleados/Edit/5
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _context.ObtenerId(new Empleado { Id = id });
            return View(cliente);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> Edit(Empleado empleado)
        {
            try
            {
                await _context.Modificar(empleado);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Empleados/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _context.ObtenerId(new Empleado { Id = id });
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Eliminar(new Empleado { Id = id });
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.EmpleadoExists(id);
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
