
using T_RESERVATION.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace T_RESERVATION.AccesoDatos;

public class EmpleadoDAL
{

    private readonly ApplicationDbContext _context;

    public EmpleadoDAL(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Empleado empleado)
    {
        empleado.Password = CalcularHashMD5(empleado.Password);
        _context.Add(empleado);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Update(Empleado empleado)
    {
        var EmpleadoDB = await _context.Empleados.FirstOrDefaultAsync(s => s.Id == empleado.Id);

        if (EmpleadoDB != null)
        {
            EmpleadoDB.Nombre = empleado.Nombre;
            EmpleadoDB.Dui = empleado.Dui;
            EmpleadoDB.Telefono = empleado.Telefono;
            EmpleadoDB.Correo = empleado.Correo;
            EmpleadoDB.Direccion = empleado.Direccion;
            EmpleadoDB.FechaNacimiento = empleado.FechaNacimiento;
            EmpleadoDB.Password = CalcularHashMD5(empleado.Password);

            _context.Update(EmpleadoDB);
        }

        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(Empleado empleado)
    {

        var EmpleadoDB = await _context.Clientes.FirstAsync(s => s.Id == empleado.Id);

        if (EmpleadoDB != null) _context.Remove(EmpleadoDB);

        return await _context.SaveChangesAsync();
    }

    private string CalcularHashMD5(string texto)
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