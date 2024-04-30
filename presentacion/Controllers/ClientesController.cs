using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T_Reservation.Models;
using Microsoft.AspNetCore.Authorization;
using T_RESERVATION.LogicaNegocio;
using T_RESERVATION.EntidadesNegocio;
using Cliente = T_RESERVATION.EntidadesNegocio.Cliente;


namespace T_Reservation.Controllers
{

    public class ClientesController : Controller
    {
        readonly LogicaNegocio _clienteBL;


        public ClientesController(LogicaNegocio logicaNegocio)
        {
            _clienteBL = logicaNegocio;
        }


        // GET: Clientes
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Index(T_RESERVATION.EntidadesNegocio.Cliente cliente)
        {
            return View(await _clienteBL.Search(cliente));
        }

        // GET: Clientes/Details/5 Administrador
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Details(int id)
        {
            var clientes = await _clienteBL.GetForId(new Cliente { Id = id });

            return View(clientes);
        }

        // GET: Clientes/Create

        public IActionResult Create()
        {
            Cliente cliente = new Cliente();
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {

            try
            {
                await _clienteBL.Create(cliente);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Edit(int id)
        {
            var estudiante = await _clienteBL.GetForId(new Cliente { Id = id });

            return View(estudiante);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Cliente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            try
            {
                await _clienteBL.Update(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Clientes/Delete/5
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _clienteBL.GetForId(new Cliente { Id = id });
            return View(estudiante);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador,Cliente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Cliente cliente)
        {
            try
        {
            await _clienteBL.Delete(cliente);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
        }

        // private bool ClienteExists(int id)
        // {
        //     // return _clienteBL..Any(e => e.Id == id);
        // }

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
    }
}
