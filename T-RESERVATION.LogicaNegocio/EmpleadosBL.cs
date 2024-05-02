using T_RESERVATION.AccesoDatos;
using T_RESERVATION.EntidadesNegocio;

namespace T_RESERVATION.AccesoDatos
{
    public class EmpleadosBL
    {
        readonly EmpleadoDAL _context;

        public EmpleadosBL(EmpleadoDAL empleado)
        {
            _context = empleado;
        }

        public async Task<int> Crear(Empleado empleado)
        {
            return await _context.Create(empleado);
        }
        public async Task<int> Modificar(Empleado empleado)
        {
            return await _context.Update(empleado);
        }
        public async Task<int> Eliminar(Empleado empleado)
        {
            return await _context.Delete(empleado);
        }

        public async Task<Empleado> ObtenerId(Empleado empleado)
        {
            return await _context.ObtenerId(empleado);
        }

        public async Task<List<Empleado>> ObtenerTodo()
        {
            return await _context.ObtenerTodo();
        }
        public bool EmpleadoExists(int id)
        {
            return _context.EmpleadoExists(id);
        }

    }
}