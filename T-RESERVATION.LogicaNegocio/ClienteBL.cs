using Microsoft.EntityFrameworkCore;
using T_RESERVATION.AccesoDatos;
using T_RESERVATION.EntidadesNegocio;

namespace T_RESERVATION.LogicaNegocio;

public class ClienteBL
{
    readonly ClientesDAL _ClienteDAL;

    public ClienteBL(ClientesDAL estudianteDAL)
    {
        _ClienteDAL = estudianteDAL;
    }

    public async Task<int> Crear(Cliente cliente)
    {
        return await _ClienteDAL.Crear(cliente);
    }

    public async Task<int> Modificar(Cliente cliente)
    {
        return await _ClienteDAL.Modificar(cliente);
    }

    public async Task<int> Eliminar(Cliente cliente)
    {
        return await _ClienteDAL.Eliminar(cliente);
    }

    public async Task<Cliente> ObtenerId(Cliente cliente)
    {
        return await _ClienteDAL.ObtenerId(cliente);
    }

    public async Task<List<Cliente>> ObtenerTodo()
    {
        return await _ClienteDAL.ObtenerTodo();
    }
    
    public async Task<List<Cliente>> Buscar(Cliente cliente)
    {
        return await _ClienteDAL.Buscar(cliente);
    }

    public bool ClienteExists(int id)
    {
        return _ClienteDAL.ClienteExists(id);
    }
}
