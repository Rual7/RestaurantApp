using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer.Repositories;

public class AllergenRepository
{
    #region Get

    public List<Allergen> GetAll()
    {
        using RestaurantDbContext context =
            new();

        return context.Allergens
            .OrderBy(
                allergen => allergen.Name)
            .ToList();
    }

    #endregion

    #region Add

    public void Add(Allergen allergen)
    {
        using RestaurantDbContext context =
            new();

        context.Database.ExecuteSqlRaw(
            "CALL sp_add_allergen({0})",
            allergen.Name);
    }

    #endregion

    #region Delete

    public void Delete(
        int id)
    {
        using RestaurantDbContext context =
            new();

        Allergen? allergen =
            context.Allergens.FirstOrDefault(
                allergen => allergen.Id == id);

        if (allergen == null)
        {
            return;
        }

        context.Allergens.Remove(allergen);

        context.SaveChanges();
    }

    #endregion
}