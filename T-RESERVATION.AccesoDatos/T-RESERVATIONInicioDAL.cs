using T_RESERVATION.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;

namespace  T_RESERVATION.AccesoDatos
{
    public class InicioDAL
    {
     private readonly ApplicationDbContext _context;
     public InicioDAL (ApplicationDbContext context)
     {
        _context = context;
     }
      public async Task<List<Menu>> GetMenuAsync(string searchString)
        {
            var menuQuery = _context.Menu.Include(m => m.Restaurante);

            if (!string.IsNullOrEmpty(searchString))
            {
                menuQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Menu, Restaurante>)menuQuery.Where(m => m.Producto.Contains(searchString));
            }

            return await menuQuery.ToListAsync();
        }

        public async Task<Menu> GetMenuDetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _context.Menu
                .Include(m => m.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> MenuExistsAsync(int id)
        {
            return await _context.Menu.AnyAsync(e => e.Id == id);
        }

        public async Task<List<Restaurante>> GetRestaurantesAsync(string searchString)
        {
            var restaurantesQuery = _context.Restaurantes.Include(r => r.Menus);

            if (!string.IsNullOrEmpty(searchString))
            {
                restaurantesQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Restaurante, ICollection<Menu>>)restaurantesQuery.Where(r => r.Nombre.Contains(searchString));
            }

            return await restaurantesQuery.ToListAsync();
        }

        public async Task<Restaurante> GetRestauranteDetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _context.Restaurantes
                .Include(r => r.Menus)
                .FirstOrDefaultAsync(r => r.IdRestaurante == id);
        }

        public async Task<bool> RestauranteExistsAsync(int id)
        {
            return await _context.Restaurantes.AnyAsync(e => e.IdRestaurante == id);
        }

    }
}