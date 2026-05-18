using DataAccessLayer.Repositories;
using Models;

namespace BusinessLogicLayer.Services;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository =
        new();

    #region Get

    public List<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    #endregion

    #region Add

    public void Add(
        Category category)
    {
        _categoryRepository.Add(category);
    }

    #endregion

    #region Delete

    public void Delete(
        int id)
    {
        _categoryRepository.Delete(id);
    }

    #endregion
}