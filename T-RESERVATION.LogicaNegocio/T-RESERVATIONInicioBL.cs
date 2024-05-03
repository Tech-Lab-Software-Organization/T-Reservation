using T_RESERVATION.AccesoDatos;
using T_RESERVATION.EntidadesNegocio;

namespace T_RESERVATION.LogicaNegocio;

public class InicioBL
{
    readonly TRESERVATIONIniciarSesion _context;

    public InicioBL(TRESERVATIONIniciarSesion contexto)
    {
        _context = contexto;
    }

    public async Task<Empleado> AutenticarEmpleadoAsync(string correo, string passwordHash)
    {
        return await _context.BuscarEmpleadoAsync(correo, passwordHash);
    }

    public async Task<Cliente> AutenticarClienteAsync(string correo, string passwordHash)
    {
        return await _context.BuscarClienteAsync(correo, passwordHash);
    }
}
