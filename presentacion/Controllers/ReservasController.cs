using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_RESERVATION.AccesoDatos;
using T_RESERVATION.EntidadesNegocio;
using T_RESERVATION.LogicaNegocio;

namespace T_Reservation.Controllers
{
    //[Authorize(Roles = "Administrador, Empleado,Cliente")]
    public class ReservasController : Controller
    {
        private readonly ReservacionBL _context;


        public ReservasController(ReservacionBL context)
        {
            _context = context;

        }

        // GET: Reservas

        public async Task<IActionResult> Index()
        {
            var restaurantes = await _context.ObtenerRestaurante();
            ViewBag.restaurante = new SelectList(restaurantes, "RestauranteId", "Producto");
            var mesa = await _context.ObtenerMesa();
            ViewBag.restaurante = new SelectList(mesa, "ClienteId", "Producto");
            var cliente = await _context.ObtenerCliente();
            ViewBag.restaurante = new SelectList(cliente, "MesaId", "Producto");
            return View(await _context.ObtenerTodo());
        }



        // GET: Reservas/Details/5

        public async Task<IActionResult> Details(int id)
        {
            var menu = await _context.ObtenerId(new Reserva { Id = id });
            if (menu == null)
            {
                return NotFound();
            }
            var restaurantes = await _context.ObtenerRestaurante();
            ViewBag.restaurante = new SelectList(restaurantes, "RestauranteId", "Producto");
            var mesa = await _context.ObtenerMesa();
            ViewBag.restaurante = new SelectList(mesa, "ClienteId", "Producto");
            var cliente = await _context.ObtenerCliente();
            ViewBag.restaurante = new SelectList(cliente, "MesaId", "Producto");
            return View(menu);
        }

        // GET: Reservas/Create
        //[Authorize(Roles = "Cliente")]
        public async Task<IActionResult> CreateAsync()
        {
            var restaurantes = await _context.ObtenerRestaurante();
            ViewData["RestauranteId"] = new SelectList(restaurantes, "IdRestaurante", "Nombre");
            var mesa = await _context.ObtenerMesa();
            ViewData["MesaId"] = new SelectList(mesa, "Id", "Numero");
            var cliente = await _context.ObtenerCliente();
            ViewData["ClienteId"] = new SelectList(cliente, "Id", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Cliente")]
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,RestauranteId,MesaId,CantidadPersonas,FechaInicio,FechaFin")] Reserva reserva)
        {
            await _context.Crear(reserva);
            return RedirectToAction(nameof(Index));

        }

        // GET: Reservas/Edit/5
        //[Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _context.ObtenerId(new Reserva { Id = id });
            if (reserva == null)
            {
                return NotFound();
            }
            var restaurantes = await _context.ObtenerRestaurante();
            ViewData["RestauranteId"] = new SelectList(restaurantes, "RestauranteId", "Producto");
            var mesa = await _context.ObtenerMesa();
            ViewData["ClienteId"] = new SelectList(mesa, "ClienteId", "Producto");
            var cliente = await _context.ObtenerCliente();
            ViewData["MesaId"] = new SelectList(cliente, "MesaId", "Producto");
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Cliente")]
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
                    await _context.Modificar(reserva);
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
            var restaurantes = await _context.ObtenerRestaurante();
            ViewBag.restaurante = new SelectList(restaurantes, "RestauranteId", "Producto");
            var mesa = await _context.ObtenerMesa();
            ViewBag.restaurante = new SelectList(mesa, "ClienteId", "Producto");
            var cliente = await _context.ObtenerCliente();
            ViewBag.restaurante = new SelectList(cliente, "MesaId", "Producto");
            return View(reserva);
        }



        // GET: Reservas/Delete/5
        //[Authorize(Roles = "Cliente, Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var reserva = await _context.ObtenerId(new Reserva { Id = id });
            if (reserva == null)
            {
                return NotFound();
            }
            var restaurantes = await _context.ObtenerRestaurante();
            ViewData["RestauranteId"] = new SelectList(restaurantes, "RestauranteId", "Producto");
            var mesa = await _context.ObtenerMesa();
            ViewData["ClienteId"] = new SelectList(mesa, "ClienteId", "Producto");
            var cliente = await _context.ObtenerCliente();
            ViewData["MesaId"] = new SelectList(cliente, "MesaId", "Producto");
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[/*Authorize(Roles = "Cliente, Administrador")]*/
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Eliminar(new Reserva { Id = id });
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.ReservaExists(id);
        }
    }
}
