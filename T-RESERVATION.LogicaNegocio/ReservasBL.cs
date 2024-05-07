using T_RESERVATION.AccesoDatos;
using T_RESERVATION.EntidadesNegocio;

namespace T_RESERVATION.LogicaNegocio
{
    public class ReservacionBL
    {
        readonly ReservasDAL _context;

        public ReservacionBL(ReservasDAL empleado)
        {
            _context = empleado;
        }

        public async Task<int> Crear(Reserva reserva)
        {
            return await _context.Crear(reserva);
        }
        public async Task<int> Modificar(Reserva reserva)
        {
            return await _context.Modificar(reserva);
        }
        public async Task<int> Eliminar(Reserva reserva)
        {
            return await _context.Eliminar(reserva);
        }

        public async Task<Reserva> ObtenerId(Reserva reserva)
        {
            return await _context.ObtenerId(reserva);
        }

        public async Task<List<Reserva>> ObtenerTodo()
        {
            return await _context.ObtenerTodo();
        }
        public bool ReservaExists(int id)
        {
            return _context.ReservaExiste(id);
        }
        public async Task<List<Restaurante>> ObtenerRestaurante()
        {
            return await _context.ObtenerRestaurante();
        }
        public async Task<List<Cliente>> ObtenerCliente()
        {
            return await _context.ObtenerCliente();
        }
        public async Task<List<Mesa>> ObtenerMesa()
        {
            return await _context.ObtenerMesa();
        }
    }

}