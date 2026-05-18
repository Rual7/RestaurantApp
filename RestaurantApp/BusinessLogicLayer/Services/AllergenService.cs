// AllergenService.cs

using DataAccessLayer.Repositories;
using Models;

namespace BusinessLogicLayer.Services;

public class AllergenService
{
    private readonly AllergenRepository _allergenRepository =
        new();

    // =====================================================
    // Get
    // =====================================================

    public List<Allergen> GetAll()
    {
        return _allergenRepository.GetAll();
    }

    // =====================================================
    // Add
    // =====================================================

    public void Add(Allergen allergen)
    {
        _allergenRepository.Add(allergen);
    }

    // =====================================================
    // Delete
    // =====================================================

    public void Delete(int id)
    {
        _allergenRepository.Delete(id);
    }
}