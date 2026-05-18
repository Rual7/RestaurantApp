using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer.Repositories;

public class MenuRepository
{
    #region Get

    public List<Menu> GetAllAvailable()
    {
        using RestaurantDbContext context =
            new();

        return context.Menus
            .Include(menu => menu.Category)
            .Include(menu => menu.MenuDishes)
                .ThenInclude(menuDish => menuDish.Dish)
            .Where(menu => menu.IsAvailable)
            .OrderBy(menu => menu.Name)
            .ToList();
    }

    #endregion
}