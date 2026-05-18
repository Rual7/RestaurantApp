using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models;

public class CartItem : INotifyPropertyChanged
{
    #region Fields

    private int _quantity;

    #endregion

    #region Properties

    public Dish? Dish
    {
        get;
        set;
    }

    public Menu? Menu
    {
        get;
        set;
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;

            OnPropertyChanged();

            OnPropertyChanged(nameof(TotalPrice));
        }
    }

    public decimal UnitPrice
    {
        get
        {
            if (Dish != null)
            {
                return Dish.Price;
            }

            if (Menu != null)
            {
                decimal total =
                    Menu.MenuDishes.Sum(
                        menuDish =>
                            menuDish.Dish.Price);

                return total -
                       (total *
                        Menu.DiscountPercent / 100);
            }

            return 0;
        }
    }

    public decimal TotalPrice =>
        UnitPrice * Quantity;

    public string DisplayName =>
        Dish?.Name ??
        Menu?.Name ??
        string.Empty;

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler?
        PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName]
        string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(
                propertyName));
    }

    #endregion
}