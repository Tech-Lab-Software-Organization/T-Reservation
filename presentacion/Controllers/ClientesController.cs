using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using T_RESERVATION.LogicaNegocio;
using Cliente = T_RESERVATION.EntidadesNegocio.Cliente;


namespace T_Reservation.Controllers
{

    public class ClientesController : Controller
    {
        readonly ClienteBL _clienteBL;


        public ClientesController(ClienteBL logicaNegocio)
        {
            _clienteBL = logicaNegocio;
        }


        // GET: Clientes
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Index()
        {
            return View(await _clienteBL.ObtenerTodo());
        }

        // GET: Clientes/Details/5 Administrador
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<IActionResult> Details(int id)
        {
            var clientes = await _clienteBL.ObtenerId(new Cliente { Id = id });

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
                await _clienteBL.Crear(cliente);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: Clientes/Edit/5
        //[Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Edit(int id)
        {
            var estudiante = await _clienteBL.ObtenerId(new Cliente { Id = id });

            return View(estudiante);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "Cliente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            try
            {
                await _clienteBL.Modificar(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Clientes/Delete/5
        //[Authorize(Roles = "Administrador,Cliente")]
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _clienteBL.ObtenerId(new Cliente { Id = id });
            return View(estudiante);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[Authorize(Roles = "Administrador,Cliente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Cliente cliente)
        {
            try
            {
                await _clienteBL.Eliminar(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool ClienteExists(int id)
        {
            return _clienteBL.ClienteExists(id);
        }


    }
}
