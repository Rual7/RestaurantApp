using DataAccessLayer.Repositories;
using Models;

namespace BusinessLogicLayer.Services;

public class MenuService
{
    #region Fields

    private readonly DishRepository _dishRepository =
        new();

    private readonly MenuRepository _menuRepository =
        new();

    #endregion

    #region Dishes

    public List<Dish> GetMenu()
    {
        return _dishRepository
            .GetAllAvailable();
    }

    #endregion

    #region Menus

    public List<Menu> GetMenus()
    {
        return _menuRepository
            .GetAllAvailable();
    }

    #endregion
}