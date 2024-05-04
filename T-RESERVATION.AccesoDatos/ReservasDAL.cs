using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_RESERVATION.EntidadesNegocio;

namespace T_RESERVATION.AccesoDatos
{
    public class ReservasDAL
    {
        private readonly ApplicationDbContext _context;

        public ReservasDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Crear(Reserva reserva)
        {
            _context.Add(reserva);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Modificar(Reserva reserva)
        {
            var ReservaDB = await _context.Reservas.FirstOrDefaultAsync(s => s.Id == reserva.Id);

            if (ReservaDB != null)
            {
                ReservaDB.CantidadPersonas = reserva.CantidadPersonas;
                ReservaDB.FechaInicio = reserva.FechaInicio;
                ReservaDB.FechaFin = reserva.FechaFin;


                _context.Update(ReservaDB);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Eliminar(Reserva reserva)
        {

            var ReservaDB = await _context.Reservas.FirstOrDefaultAsync(s => s.Id == reserva.Id);

            if (ReservaDB != null) _context.Remove(ReservaDB);

            return await _context.SaveChangesAsync();
        }

        public async Task<Reserva> ObtenerId(Reserva reserva)
        {
            var ReservaDB = await _context.Reservas.FirstOrDefaultAsync(s => s.Id == reserva.Id);
            if (ReservaDB != null)
            {
                return ReservaDB;
            }
            else
                return new Reserva();

        }
        public async Task<List<Reserva>> ObtenerTodo()
        {
            return _context.Reservas != null ?

            await _context.Reservas.ToListAsync() :
            new List<Reserva>();
        }

        
    }
}

