using T_RESERVATION.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;


namespace T_RESERVATION.AccesoDatos;

public class MenuDAL{
    private readonly ApplicationDbContext _context;

    public MenuDAL(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Menu>> Menu()
        {
            return await _context.Menu.Include(m => m.Restaurante).ToListAsync();
        }
        public async Task<Menu> GetMenu(int id)
        {
            return await _context.Menu.Include(m => m.Restaurante).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CrearMenu(Menu menu)
        {
            
            _context.Add(menu);
            await _context.SaveChangesAsync();
        }

        public async Task ModificarMenu(Menu menu)
        {
            var existingMenu = await _context.Menu.FirstOrDefaultAsync(s => s.Id == menu.Id);

            if (existingMenu == null)
            {
                throw new InvalidOperationException("Menu not found");
            }

            

            existingMenu.Producto = menu.Producto;
            existingMenu.Descripcion = menu.Descripcion;
            existingMenu.NotaEspecial = menu.NotaEspecial;
            existingMenu.Precio = menu.Precio;
            existingMenu.RestauranteId = menu.RestauranteId;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                throw new InvalidOperationException("Menu not found");
            }
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
        }
}