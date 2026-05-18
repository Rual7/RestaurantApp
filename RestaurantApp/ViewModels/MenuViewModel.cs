using BusinessLogicLayer.Services;
using Models;
using System.Collections.ObjectModel;

namespace ViewModels;

public class MenuViewModel : BaseViewModel
{
    private readonly MenuService _menuService;

    private List<Dish> _allDishes =
        new();

    public MenuViewModel()
    {
        _menuService = new MenuService();

        LoadMenu();
    }

    // =========================================================
    // Properties
    // =========================================================

    private ObservableCollection<Dish> _dishes =
        new();

    public ObservableCollection<Dish> Dishes
    {
        get => _dishes;
        set => SetProperty(ref _dishes, value);
    }

    private ObservableCollection<string> _categories =
        new()
        {
            "All Categories"
        };

    public ObservableCollection<string> Categories
    {
        get => _categories;
        set => SetProperty(ref _categories, value);
    }

    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                FilterDishes();
            }
        }
    }

    private string _selectedCategory =
        "All Categories";

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (SetProperty(ref _selectedCategory, value))
            {
                FilterDishes();
            }
        }
    }

    // =========================================================
    // Allergen Filters
    // =========================================================

    private bool _excludeGluten;

    public bool ExcludeGluten
    {
        get => _excludeGluten;
        set
        {
            if (SetProperty(ref _excludeGluten, value))
            {
                FilterDishes();
            }
        }
    }

    private bool _excludeLactose;

    public bool ExcludeLactose
    {
        get => _excludeLactose;
        set
        {
            if (SetProperty(ref _excludeLactose, value))
            {
                FilterDishes();
            }
        }
    }

    private bool _excludeEggs;

    public bool ExcludeEggs
    {
        get => _excludeEggs;
        set
        {
            if (SetProperty(ref _excludeEggs, value))
            {
                FilterDishes();
            }
        }
    }

    // =========================================================
    // Methods
    // =========================================================

    private void LoadMenu()
    {
        _allDishes =
            _menuService.GetMenu();

        Dishes = new ObservableCollection<Dish>(
            _allDishes);

        List<string> categories =
            _allDishes
                .Where(dish => dish.Category != null)
                .Select(dish => dish.Category.Name)
                .Distinct()
                .OrderBy(category => category)
                .ToList();

        foreach (string category in categories)
        {
            Categories.Add(category);
        }
    }

    private void FilterDishes()
    {
        IEnumerable<Dish> filtered =
            _allDishes;

        // Search

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(
                dish =>
                    dish.Name.Contains(
                        SearchText,
                        StringComparison.OrdinalIgnoreCase));
        }

        // Category

        if (SelectedCategory != "All Categories")
        {
            filtered = filtered.Where(
                dish =>
                    dish.Category != null &&
                    dish.Category.Name == SelectedCategory);
        }

        // Allergens

        if (ExcludeGluten)
        {
            filtered = filtered.Where(
                dish =>
                    !dish.DishAllergens.Any(
                        dishAllergen =>
                            dishAllergen.Allergen.Name
                                == "Gluten"));
        }

        if (ExcludeLactose)
        {
            filtered = filtered.Where(
                dish =>
                    !dish.DishAllergens.Any(
                        dishAllergen => 
                            dishAllergen.Allergen.Name
                                == "Lactoză"));
        }

        if (ExcludeEggs)
        {
            filtered = filtered.Where(
                dish =>
                    !dish.DishAllergens.Any(
                        dishAllergen =>
                            dishAllergen.Allergen.Name
                                == "Ouă"));
        }

        Dishes = new ObservableCollection<Dish>(
            filtered);
    }
}