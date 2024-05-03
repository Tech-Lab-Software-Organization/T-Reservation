
using T_RESERVATION.AccesoDatos;
using System.Security.Claims;
using T_RESERVATION.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;

namespace T_RESERVATION.AccesoDatos
{

   public class TRESERVATIONIniciarSesion
    {
        private readonly ApplicationDbContext _context;

        public TRESERVATIONIniciarSesion(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Empleado> BuscarEmpleadoAsync(string correo, string passwordHash)
        {
            return await _context.Empleados.FirstOrDefaultAsync(e => e.Correo == correo && e.Password == passwordHash);
        }

        public async Task<Cliente> BuscarClienteAsync(string correo, string passwordHash)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == correo && c.Password == passwordHash);
        }

    }
}