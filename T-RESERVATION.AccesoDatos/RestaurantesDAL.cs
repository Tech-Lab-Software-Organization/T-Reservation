using T_RESERVATION.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace T_RESERVATION.AccesoDatos
{
    public class RestaurantesDAL
    {
        private readonly ApplicationDbContext _context;

        public RestaurantesDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurante>> ObtenerTodo()
        {
            return _context.Restaurantes != null ?

       await _context.Restaurantes.ToListAsync() :
       new List<Restaurante>();
        }
        public async Task<Restaurante> ObtenerId(Restaurante restaurante)
        {
            var facturaRestaurante = await _context.Restaurantes
              .Include(f => f.EmpleadoId)
              .Include(s => s.Menus)
              .FirstOrDefaultAsync(m => m.IdRestaurante == restaurante.IdRestaurante);
            if (facturaRestaurante != null)
                return facturaRestaurante;
            else
            {

            }
            return new Restaurante();
        }
        private byte[] LeerArchivo(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                archivo.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public async Task<List<Empleado>> ObtenerEmpleado()
        {
            return await _context.Empleados.ToListAsync();
        }

        public async Task<List<Mesa>> ObtenerMesa()
        {
            return await _context.Mesas.ToListAsync();
        }
        public async Task<int> Crear(Restaurante restaurante, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    restaurante.Imagen = memoryStream.ToArray();
                }
            }


            _context.Add(restaurante);
            return await _context.SaveChangesAsync();
        }
        //public async Task<int> Modificar(Restaurante restaurante, IFormFile imagen)
        //{
        //    if (imagen != null && imagen.Length > 0)
        //    {
        //        // Eliminar la imagen actual del producto si existe
        //        if (restaurante.Imagen != null && restaurante.Imagen.Length > 0)
        //        {
        //            restaurante.Imagen = null; // Eliminar la imagen existente
        //        }

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await imagen.CopyToAsync(memoryStream);
        //            restaurante.Imagen = memoryStream.ToArray(); // Asignar la nueva imagen
        //        }
        //    }

        //    var RestauranteDB = await _context.Restaurantes.FirstOrDefaultAsync(s => s.IdRestaurante == restaurante.IdRestaurante);

        //    if (RestauranteDB != null)
        //    {
        //        RestauranteDB.Nombre = restaurante.Nombre;
        //        RestauranteDB.Descripcion = restaurante.Descripcion;
        //        RestauranteDB.Direccion = restaurante.Direccion;
                


        //        _context.Update(RestauranteDB);
        //    }

        //    return await _context.SaveChangesAsync();
        //}
        public async Task<int> Eliminar(Restaurante restaurante)
        {
            var eliminar = await _context.Restaurantes
               .FindAsync(restaurante.IdRestaurante);
            if (restaurante != null)
            {
                _context.Restaurantes.Remove(eliminar);
            }
            return await _context.SaveChangesAsync();
        }
        public bool RestauranteExists(int id)
        {
            return (_context.Restaurantes?.Any(e => e.IdRestaurante == id)).GetValueOrDefault();
        }








    }

}
