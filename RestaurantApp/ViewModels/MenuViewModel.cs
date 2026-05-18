using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Services;
using Models;
using RestaurantApp.Views.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels;

public class MenuViewModel
    : BaseViewModel
{
    private readonly MenuService _menuService =
        new();

    private readonly CartService _cartService =
        CartService.Instance;

    private ObservableCollection<Dish> _dishes =
        [];

    private ObservableCollection<Menu> _menus =
    [];

    public ObservableCollection<Menu> Menus
    {
        get => _menus;
        set => SetProperty(ref _menus, value);
    }

    private string _searchText =
        string.Empty;

    private List<string> _categories =
        [];

    private string _selectedCategory =
        "All Categories";

    private List<string> _allergens =
        [];

    private string _selectedAllergen =
        "All Allergens";

    public MenuViewModel()
    {
        AddToCartCommand =
            new RelayCommand(
                AddToCart);

        AddMenuToCartCommand =
            new RelayCommand(
                AddMenuToCart);

        LoadCategories();

        LoadAllergens();

        LoadDishes();

        LoadMenus();
    }

    #region Dishes

    public ObservableCollection<Dish> Dishes
    {
        get => _dishes;

        set => SetProperty(
            ref _dishes,
            value);
    }

    #endregion

    #region Search

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

    #endregion

    #region Categories

    public List<string> Categories
    {
        get => _categories;

        set => SetProperty(
            ref _categories,
            value);
    }

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

    #endregion

    #region Allergens

    public List<string> Allergens
    {
        get => _allergens;

        set => SetProperty(
            ref _allergens,
            value);
    }

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

    #endregion

    #region Client

    public bool IsClient =>
        SessionManager.IsAuthenticated;

    #endregion

    #region Commands

    public ICommand AddToCartCommand
    {
        get;
    }
    public ICommand AddMenuToCartCommand
    {
        get;
    }

    #endregion

    #region Load Categories

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

    #endregion

    #region Load Allergens

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
                    allergen =>
                        allergen.Allergen.Name)

                .Distinct()

                .OrderBy(
                    allergen => allergen)
        ];
    }

    #endregion

    #region Load Dishes

    private void LoadDishes()
    {
        Dishes =
            new ObservableCollection<Dish>(
                _menuService.GetMenu());
    }

    #endregion

    #region Load Menus
    private void LoadMenus()
    {
        Menus =
            new ObservableCollection<Menu>(
                _menuService.GetMenus());
    }

    #endregion

    #region Filters

    private void ApplyFilters()
    {
        IEnumerable<Dish> dishes =
            _menuService.GetMenu();

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

        if (SelectedCategory !=
            "All Categories")
        {
            dishes = dishes.Where(
                dish =>
                    dish.Category.Name ==
                    SelectedCategory);
        }

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

    #endregion

    #region Add To Cart

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

    #endregion

    #region Add Menu To Cart

    private void AddMenuToCart(
    object? parameter)
    {
        if (parameter is not Menu menu)
        {
            return;
        }

        _cartService.AddMenuToCart(menu);

        CustomMessageBox.Show(
            "Menu",
            $"{menu.Name} added to cart.");
    }

    #endregion
}