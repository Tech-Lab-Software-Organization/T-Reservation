﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_Reservation.Models;

namespace T_Reservation.Controllers
{
    public class MesasController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public MesasController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Mesas
        public async Task<IActionResult> Index()
        {
            var usuarioRol = _httpContextAccessor.HttpContext.Session.GetString("UsuarioRol");


            if (usuarioRol == "administrador" || usuarioRol == "Cliente")
            {
                var applicationDbContext = _context.Mesas.Include(m => m.Restaurante);
                return View(await applicationDbContext.ToListAsync());
            }

            else if (usuarioRol == "Empleado"){

                int usuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId") ?? default(int);
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == usuarioId);


                if (cliente != null)
                {
                    var mesas = await _context.Mesas
                        .Include(m => m.Restaurante)
               .Where(m => m.Restaurante.EmpleadoId == usuarioId)
                    .ToListAsync();


                    foreach (var mesa in mesas)
                    {
                        Console.WriteLine($"Menu ID: {mesa.Id}, Restaurant Name: {mesa.Restaurante.Nombre}");
                    }
                    return View(mesas);
                }
            }

            return RedirectToAction("Index", "Login");
        }

        // GET: Mesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mesas == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas
                .Include(m => m.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // GET: Mesas/Create
        public IActionResult Create()
        {
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre");
            return View();
        }

        // POST: Mesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Capacidad,Area,Disponibilidad,RestauranteId")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre", mesa.RestauranteId);
            return View(mesa);
        }

        // GET: Mesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mesas == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre", mesa.RestauranteId);
            return View(mesa);
        }

        // POST: Mesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Capacidad,Area,Disponibilidad,RestauranteId")] Mesa mesa)
        {
            if (id != mesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesaExists(mesa.Id))
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
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Nombre", mesa.RestauranteId);
            return View(mesa);
        }

        // GET: Mesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mesas == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas
                .Include(m => m.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mesas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mesas'  is null.");
            }
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa != null)
            {
                _context.Mesas.Remove(mesa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesaExists(int id)
        {
          return _context.Mesas.Any(e => e.Id == id);
        }
    }
}
