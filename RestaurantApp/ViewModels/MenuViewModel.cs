// MenuViewModel.cs

using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Services;
using Models;
using RestaurantApp.Views.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels;

public class MenuViewModel : BaseViewModel
{
    private readonly MenuService _menuService;

    private readonly CartService _cartService;

    public MenuViewModel()
    {
        _menuService =
            new MenuService();

        _cartService =
            new CartService();

        AddToCartCommand =
            new RelayCommand(
                AddToCart);

        LoadCategories();

        LoadAllergens();

        LoadDishes();
    }

    // =====================================================
    // Dishes
    // =====================================================

    private ObservableCollection<Dish> _dishes =
        [];

    public ObservableCollection<Dish> Dishes
    {
        get => _dishes;
        set => SetProperty(ref _dishes, value);
    }

    // =====================================================
    // Search
    // =====================================================

    private string _searchText =
        string.Empty;

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(
                    ref _searchText,
                    value))
            {
                ApplyFilters();
            }
        }
    }

    // =====================================================
    // Categories
    // =====================================================

    private List<string> _categories =
        [];

    public List<string> Categories
    {
        get => _categories;
        set => SetProperty(ref _categories, value);
    }

    private string _selectedCategory =
        "All Categories";

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (SetProperty(
                    ref _selectedCategory,
                    value))
            {
                ApplyFilters();
            }
        }
    }

    // =====================================================
    // Allergens
    // =====================================================

    private List<string> _allergens =
        [];

    public List<string> Allergens
    {
        get => _allergens;
        set => SetProperty(ref _allergens, value);
    }

    private string _selectedAllergen =
        "All Allergens";

    public string SelectedAllergen
    {
        get => _selectedAllergen;
        set
        {
            if (SetProperty(
                    ref _selectedAllergen,
                    value))
            {
                ApplyFilters();
            }
        }
    }

    // =====================================================
    // Client
    // =====================================================

    public bool IsClient =>
        SessionManager.IsAuthenticated;

    // =====================================================
    // Commands
    // =====================================================

    public ICommand AddToCartCommand
    {
        get;
    }

    // =====================================================
    // Load Categories
    // =====================================================

    private void LoadCategories()
    {
        Categories =
        [
            "All Categories",

            .._menuService
                .GetMenu()
                .Select(
                    dish => dish.Category.Name)
                .Distinct()
                .OrderBy(
                    category => category)
        ];
    }

    // =====================================================
    // Load Allergens
    // =====================================================

    private void LoadAllergens()
    {
        Allergens =
        [
            "All Allergens",

            .._menuService
                .GetMenu()
                .SelectMany(
                    dish => dish.DishAllergens)
                .Select(
                    allergen => allergen.Allergen.Name)
                .Distinct()
                .OrderBy(
                    allergen => allergen)
        ];
    }

    // =====================================================
    // Load Dishes
    // =====================================================

    private void LoadDishes()
    {
        Dishes =
            new ObservableCollection<Dish>(
                _menuService.GetMenu());
    }

    // =====================================================
    // Filters
    // =====================================================

    private void ApplyFilters()
    {
        IEnumerable<Dish> dishes =
            _menuService.GetMenu();

        // =================================================
        // Search
        // =================================================

        if (!string.IsNullOrWhiteSpace(
                SearchText))
        {
            dishes = dishes.Where(
                dish =>
                    dish.Name.Contains(
                        SearchText,
                        StringComparison
                            .OrdinalIgnoreCase));
        }

        // =================================================
        // Category
        // =================================================

        if (SelectedCategory !=
            "All Categories")
        {
            dishes = dishes.Where(
                dish =>
                    dish.Category.Name ==
                    SelectedCategory);
        }

        // =================================================
        // Allergen
        // =================================================

        if (SelectedAllergen !=
            "All Allergens")
        {
            dishes = dishes.Where(
                dish =>
                    !dish.DishAllergens.Any(
                        allergen =>
                            allergen.Allergen.Name ==
                            SelectedAllergen));
        }

        Dishes =
            new ObservableCollection<Dish>(
                dishes);
    }

    // =====================================================
    // Add To Cart
    // =====================================================

    private void AddToCart(
        object? parameter)
    {
        if (parameter is not Dish dish)
        {
            return;
        }

        _cartService.AddToCart(
            dish);

        CustomMessageBox.Show(
            "Cart",
            $"{dish.Name} added to cart.");
    }
}