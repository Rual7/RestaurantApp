// CategoryService.cs

using DataAccessLayer.Repositories;
using Models;

namespace BusinessLogicLayer.Services;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository =
        new();

    // =====================================================
    // Get
    // =====================================================

    public List<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    // =====================================================
    // Add
    // =====================================================

    public void Add(Category category)
    {
        _categoryRepository.Add(category);
    }

    // =====================================================
    // Delete
    // =====================================================

    public void Delete(int id)
    {
        _categoryRepository.Delete(id);
    }
}