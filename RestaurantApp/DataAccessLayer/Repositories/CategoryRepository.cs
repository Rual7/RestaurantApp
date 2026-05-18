using DataAccessLayer.Context;
using Models;

namespace DataAccessLayer.Repositories;

public class CategoryRepository
{
    #region Get

    public List<Category> GetAll()
    {
        using RestaurantDbContext context =
            new();

        return context.Categories
            .OrderBy(
                category => category.Name)
            .ToList();
    }

    #endregion

    #region Add

    public void Add(
        Category category)
    {
        using RestaurantDbContext context =
            new();

        context.Categories.Add(category);

        context.SaveChanges();
    }

    #endregion

    #region Delete

    public void Delete(
        int id)
    {
        using RestaurantDbContext context =
            new();

        Category? category =
            context.Categories.FirstOrDefault(
                category => category.Id == id);

        if (category == null)
        {
            return;
        }

        context.Categories.Remove(category);

        context.SaveChanges();
    }

    #endregion
}