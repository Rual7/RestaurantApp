using BusinessLogicLayer.Services;
using Models;
using Models.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels;

public class EmployeeViewModel
    : BaseViewModel
{
    private readonly CategoryService _categoryService =
        new();

    private readonly AllergenService _allergenService =
        new();

    private readonly MenuService _menuService =
        new();

    private string _newCategoryName =
        string.Empty;

    private string _newAllergenName =
        string.Empty;

    private EmployeeSection _currentSection =
        EmployeeSection.Categories;

    public EmployeeViewModel()
    {
        Categories =
            new ObservableCollection<Category>(
                _categoryService.GetAll());

        Allergens =
            new ObservableCollection<Allergen>(
                _allergenService.GetAll());

        LowStockDishes =
            new ObservableCollection<Dish>(
                _menuService.GetMenu()
                    .Where(
                        dish => dish.TotalQuantity <= 5));

        AddCategoryCommand =
            new RelayCommand(
                _ => AddCategory());

        DeleteCategoryCommand =
            new RelayCommand(
                category =>
                    DeleteCategory(
                        (Category)category!));

        AddAllergenCommand =
            new RelayCommand(
                _ => AddAllergen());

        DeleteAllergenCommand =
            new RelayCommand(
                allergen =>
                    DeleteAllergen(
                        (Allergen)allergen!));

        ShowCategoriesCommand =
            new RelayCommand(
                _ => CurrentSection =
                    EmployeeSection.Categories);

        ShowAllergensCommand =
            new RelayCommand(
                _ => CurrentSection =
                    EmployeeSection.Allergens);

        ShowLowStockCommand =
            new RelayCommand(
                _ => CurrentSection =
                    EmployeeSection.LowStock);
    }

    #region Collections

    public ObservableCollection<Category> Categories
    {
        get;
    }

    public ObservableCollection<Allergen> Allergens
    {
        get;
    }

    public ObservableCollection<Dish> LowStockDishes
    {
        get;
    }

    #endregion

    #region Inputs

    public string NewCategoryName
    {
        get => _newCategoryName;

        set
        {
            _newCategoryName = value;

            OnPropertyChanged();
        }
    }

    public string NewAllergenName
    {
        get => _newAllergenName;

        set
        {
            _newAllergenName = value;

            OnPropertyChanged();
        }
    }

    #endregion

    #region Sections

    public EmployeeSection CurrentSection
    {
        get => _currentSection;

        set
        {
            _currentSection = value;

            OnPropertyChanged();

            OnPropertyChanged(
                nameof(IsCategoriesVisible));

            OnPropertyChanged(
                nameof(IsAllergensVisible));

            OnPropertyChanged(
                nameof(IsLowStockVisible));
        }
    }

    public bool IsCategoriesVisible =>
        CurrentSection ==
        EmployeeSection.Categories;

    public bool IsAllergensVisible =>
        CurrentSection ==
        EmployeeSection.Allergens;

    public bool IsLowStockVisible =>
        CurrentSection ==
        EmployeeSection.LowStock;

    #endregion

    #region Commands

    public ICommand AddCategoryCommand
    {
        get;
    }

    public ICommand DeleteCategoryCommand
    {
        get;
    }

    public ICommand AddAllergenCommand
    {
        get;
    }

    public ICommand DeleteAllergenCommand
    {
        get;
    }

    public ICommand ShowCategoriesCommand
    {
        get;
    }

    public ICommand ShowAllergensCommand
    {
        get;
    }

    public ICommand ShowLowStockCommand
    {
        get;
    }

    #endregion

    #region Categories

    private void AddCategory()
    {
        if (string.IsNullOrWhiteSpace(
                NewCategoryName))
        {
            return;
        }

        Category category =
            new()
            {
                Name = NewCategoryName
            };

        _categoryService.Add(category);

        Categories.Add(category);

        NewCategoryName =
            string.Empty;
    }

    private void DeleteCategory(
        Category category)
    {
        _categoryService.Delete(
            category.Id);

        Categories.Remove(category);
    }

    #endregion

    #region Allergens

    private void AddAllergen()
    {
        if (string.IsNullOrWhiteSpace(
                NewAllergenName))
        {
            return;
        }

        Allergen allergen =
            new()
            {
                Name = NewAllergenName
            };

        _allergenService.Add(allergen);

        Allergens.Add(allergen);

        NewAllergenName =
            string.Empty;
    }

    private void DeleteAllergen(
        Allergen allergen)
    {
        _allergenService.Delete(
            allergen.Id);

        Allergens.Remove(allergen);
    }

    #endregion
}