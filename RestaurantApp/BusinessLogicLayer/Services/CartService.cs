using Models;
using System.Collections.ObjectModel;

namespace BusinessLogicLayer.Services;

public class CartService
{
    private static readonly CartService _instance =
        new();

    public static CartService Instance =>
        _instance;

    private readonly ObservableCollection<CartItem> _items =
        new();

    public ObservableCollection<CartItem> Items =>
        _items;

    public event Action? CartChanged;

    public decimal Total =>
        _items.Sum(
            item => item.TotalPrice);

    // =====================================================
    // Add
    // =====================================================

    public void AddToCart(Dish dish)
    {
        CartItem? existingItem =
            _items.FirstOrDefault(
                item => item.Dish.Id == dish.Id);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            _items.Add(
                new CartItem
                {
                    Dish = dish,
                    Quantity = 1
                });
        }

        NotifyCartChanged();
    }

    // =====================================================
    // Remove
    // =====================================================

    public void RemoveFromCart(CartItem item)
    {
        _items.Remove(item);

        NotifyCartChanged();
    }

    // =====================================================
    // Quantity
    // =====================================================

    public void IncreaseQuantity(CartItem item)
    {
        item.Quantity++;

        NotifyCartChanged();
    }

    public void DecreaseQuantity(CartItem item)
    {
        item.Quantity--;

        if (item.Quantity <= 0)
        {
            _items.Remove(item);
        }

        NotifyCartChanged();
    }

    // =====================================================
    // Clear
    // =====================================================

    public void ClearCart()
    {
        _items.Clear();

        NotifyCartChanged();
    }

    // =====================================================
    // Events
    // =====================================================

    private void NotifyCartChanged()
    {
        CartChanged?.Invoke();
    }
}