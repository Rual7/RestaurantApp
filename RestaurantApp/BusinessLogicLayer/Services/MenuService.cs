using DataAccessLayer.Repositories;
using Models;

namespace BusinessLogicLayer.Services;

public class MenuService
{
    private readonly DishRepository _dishRepository;

    public MenuService()
    {
        _dishRepository = new DishRepository();
    }

    public List<Dish> GetMenu()
    {
        return _dishRepository.GetAllAvailable();
    }
}