
using T_RESERVATION.EntidadesNegocio;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace T_RESERVATION.AccesoDatos;

public class ClientesDAL
{
    private readonly ApplicationDbContext _context;

    public ClientesDAL(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Crear(Cliente cliente)
    {

        cliente.Password = CalcularHashMD5(cliente.Password);
        _context.Add(cliente);
        await _context.SaveChangesAsync();
        return await _context.SaveChangesAsync();
    }


    public async Task<int> Modificar(Cliente cliente)
    {

        var ClienteDB = await _context.Clientes.FirstOrDefaultAsync(s => s.Id == cliente.Id);

        if (ClienteDB != null)
        {
            ClienteDB.Nombre = cliente.Nombre;
            ClienteDB.Dui = cliente.Dui;
            ClienteDB.Telefono = cliente.Telefono;
            ClienteDB.Correo = cliente.Correo;
            ClienteDB.Direccion = cliente.Direccion;
            ClienteDB.FechaNacimiento = cliente.FechaNacimiento;
            ClienteDB.Password = cliente.Password;

            _context.Update(ClienteDB);
        }

        return await _context.SaveChangesAsync();
    }

    public async Task<int> Eliminar(Cliente cliente)
    {

        var ClienteDB = await _context.Clientes.FirstOrDefaultAsync(s => s.Id == cliente.Id);

        if (ClienteDB != null) _context.Remove(ClienteDB);

        return await _context.SaveChangesAsync();
    }
  

    public async Task<Cliente> ObtenerId(Cliente cliente)
    {
        var clientea = await _context.Clientes.FirstOrDefaultAsync(s => s.Id == cliente.Id);
        if (clientea != null)
        {
            return clientea;
        }
        else
            return new Cliente();
        
    }

    public async Task<List<Cliente>> ObtenerTodo()
    {
        return _context.Clientes != null ?

        await _context.Clientes.ToListAsync() :
        new List<Cliente>();
    }

    public async Task<List<Cliente>> Buscar(Cliente cliente)
    {
        var query =  _context.Clientes.AsQueryable();
        if(!string.IsNullOrWhiteSpace(cliente.Nombre))
        {
            query = query.Where(s => s.Nombre.Contains(cliente.Nombre));
        }
        return await query.ToListAsync();

    }

    

    public static string CalcularHashMD5(string texto)
    {
        using (MD5 md5 = MD5.Create())
        {
            //Convierte la cadena de texto a bytes
            byte[] inputbytes = Encoding.UTF8.GetBytes(texto);

            //Calcula el hash MD5 de los bytes
            byte[] HashBytes = md5.ComputeHash(inputbytes);

            //convierte el hash a una cadena hexadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < HashBytes.Length; i++)
            {
                sb.Append(HashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
