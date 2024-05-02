using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using T_RESERVATION.AccesoDatos;
using T_RESERVATION.EntidadesNegocio;

namespace T_RESERVATION.LogicaNegocio
{
    public class MenuBL
    {
        readonly MenuDAL _menuDAl;
        public MenuBL(MenuDAL menuDal)
        {
            _menuDAl = menuDal;
        }
        public async Task<List<Menu>> ObtenerTodo()
        {
            return await _menuDAl.ObtenerTodo();
        }
        public async Task<Menu> ObtenerId(Menu menu)
        {
            return await _menuDAl.ObtenerId(menu);
        }
        public async Task<List<Restaurante>> ObtenerRestaurante()
        {
            return await _menuDAl.ObtenerRestaurante();
        }
        public async Task<int> Crear(Menu menu, IFormFile imagen)
        {
            return await _menuDAl.Crear(menu, imagen);
        }
        public async Task<int> Modificar(Menu menu, IFormFile imagen)
        {
            return await _menuDAl.Modificar(menu, imagen);
        }

        public async Task<int> Eliminar(Menu menu)
        {
            return await _menuDAl.EliminarMenu(menu.Id);
        }

        public bool MenuExists(int id)
        {
            return _menuDAl.MenuExiste(id);
        }

    }
}
