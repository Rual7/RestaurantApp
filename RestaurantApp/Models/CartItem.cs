using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models;

public class CartItem : INotifyPropertyChanged
{
    private int _quantity;

    public Dish Dish { get; set; }

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

    public decimal TotalPrice =>
        Dish.Price * Quantity;

    // =====================================================
    // INotifyPropertyChanged
    // =====================================================

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
}