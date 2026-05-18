using DataAccessLayer.Repositories;
using Models;

namespace BusinessLogicLayer.Services;

public class AllergenService
{
    private readonly AllergenRepository _allergenRepository =
        new();

    #region Get

    public List<Allergen> GetAll()
    {
        return _allergenRepository.GetAll();
    }

    #endregion

    #region Add

    public void Add(
        Allergen allergen)
    {
        _allergenRepository.Add(allergen);
    }

    #endregion

    #region Delete

    public void Delete(
        int id)
    {
        _allergenRepository.Delete(id);
    }

    #endregion
}