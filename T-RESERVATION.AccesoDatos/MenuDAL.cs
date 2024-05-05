using T_RESERVATION.EntidadesNegocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace T_RESERVATION.AccesoDatos;

public class MenuDAL{
    private readonly ApplicationDbContext _context;

    public MenuDAL(ApplicationDbContext context)
        {
            _context = context;
        }
    public async Task<List<Menu>> ObtenerTodo()
    {
        IQueryable<Menu> restaurantes = _context.Menu;

        restaurantes = restaurantes
                                 .Include(p => p.RestauranteId);
        return _context.Menu != null ?

     await _context.Menu.ToListAsync() :
     new List<Menu>();
    }


    

        public async Task Modificar(Menu menu)
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

        

    public async Task<Menu> ObtenerId(Menu menu)
    {
        var clientea = await _context.Menu.FirstOrDefaultAsync(s => s.Id == menu.Id);
        if (clientea != null)
        {
            return clientea;
        }
        else
            return new Menu();

    }
    public async Task<List<Restaurante>> ObtenerRestaurante()
    {
         return await _context.Restaurantes.ToListAsync();

       
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
    public async Task<int> Crear(Menu menu, IFormFile imagen)
    {
        if (imagen != null && imagen.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imagen.CopyToAsync(memoryStream);
                menu.Imagen = memoryStream.ToArray();
            }
        }


        _context.Add(menu);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Modificar(Menu menu, IFormFile imagen)
    {
        if (imagen != null && imagen.Length > 0)
        {
            // Eliminar la imagen actual del producto si existe
            if (menu.Imagen != null && menu.Imagen.Length > 0)
            {
                menu.Imagen = null; // Eliminar la imagen existente
            }

            using (var memoryStream = new MemoryStream())
            {
                await imagen.CopyToAsync(memoryStream);
                menu.Imagen = memoryStream.ToArray(); // Asignar la nueva imagen
            }
        }

        // Actualizar el producto en el contexto
        _context.Update(menu);

        // Guardar los cambios en la base de datos
        return await _context.SaveChangesAsync();
    }

    public async Task<int> EliminarMenu(int id)
    {
        var productoActual = await _context.Menu.FirstOrDefaultAsync(p => p.Id == id);

        if (productoActual == null)
        {
            throw new ArgumentException("Producto no encontrado");
        }

        _context.Remove(productoActual);
        return await _context.SaveChangesAsync();
    }

    public bool MenuExiste(int id)
    {
        return (_context.Menu?.Any(p => p.Id == id)).GetValueOrDefault();
    }
}