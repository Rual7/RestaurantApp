using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
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
        using RestaurantDbContext context =
            new();

        return context.Dishes
            .FromSqlRaw(
                "SELECT * FROM sp_get_menu()")
            .Include(dish => dish.Category)
            .Include(dish => dish.DishAllergens)
                .ThenInclude(da => da.Allergen)
            .ToList();
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