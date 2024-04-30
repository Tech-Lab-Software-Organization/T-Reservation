
using T_RESERVATION.AccesoDatos;
using T_RESERVATION.EntidadesNegocio;

namespace T_RESERVATION.LogicaNegocio;

public class LogicaNegocio
{
    readonly ClientesDAL _ClienteDAL;

    public LogicaNegocio(ClientesDAL estudianteDAL)
    {
        _ClienteDAL = estudianteDAL;
    }

    public async Task<int> Create(Cliente cliente)
    {
        return await _ClienteDAL.Create(cliente);
    }

    public async Task<int> Update(Cliente cliente)
    {
        return await _ClienteDAL.Update(cliente);
    }

    public async Task<int> Delete(Cliente cliente)
    {
        return await _ClienteDAL.Delete(cliente);
    }

    public async Task<Cliente> GetForId(Cliente cliente)
    {
        return await _ClienteDAL.GetForId(cliente);
    }

    public async Task<List<Cliente>> GetAll()
    {
        return await _ClienteDAL.GetAll();
    }
    
    public async Task<List<Cliente>> Search(Cliente cliente)
    {
        return await _ClienteDAL.Search(cliente);
    }
}
