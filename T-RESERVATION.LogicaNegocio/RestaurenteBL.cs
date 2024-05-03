using T_RESERVATION.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace T_RESERVATION.AccesoDatos
{
    public class RestauranteBL
    {
        readonly RestaurantesDAL _context;

        public RestauranteBL(RestaurantesDAL restauranteDAL)
        {
            _context = restauranteDAL;
        }

        public async Task<int> Crear(Restaurante restaurante, IFormFile imagen)
        {
            return await _context.Crear(restaurante, imagen);
        }
        //public async Task<int> Modificar(Restaurante restaurante, IFormFile imagen)
        //{
        //    return await _context.Modificar(restaurante, imagen);
        //}
        public async Task<int> Eliminar(Restaurante restaurante)
        {
            return await _context.Eliminar(restaurante);
        }

        public async Task<Restaurante> ObtenerId(Restaurante restaurante)
        {
            return await _context.ObtenerId(restaurante);
        }

        public async Task<List<Restaurante>> ObtenerTodo()
        {
            return await _context.ObtenerTodo();
        }
        public bool RestauranteExists(int id)
        {
            return _context.RestauranteExists(id);
        }
        public async Task<List<Empleado>> ObtenerEmpleado()
        {
            return await _context.ObtenerEmpleado();
        }

        public async Task<List<Mesa>> ObtenerMesa()
        {
            return await _context.ObtenerMesa();
        }
    }
}