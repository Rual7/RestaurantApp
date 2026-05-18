using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer.Repositories;

public class DishRepository
{
    public List<Dish> GetAllAvailable()
    {
        using RestaurantDbContext context = new();

        return context.Dishes
            .Include(dish => dish.Category)
            .Include(dish => dish.DishAllergens)
                .ThenInclude(
                    dishAllergen => dishAllergen.Allergen)
            .Where(dish => dish.IsAvailable)
            .OrderBy(dish => dish.Name)
            .ToList();
    }
}