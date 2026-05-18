using Models;
using System.Collections.ObjectModel;

namespace BusinessLogicLayer.Services;

public class CartService
{
    private static readonly CartService _instance =
        new();

    private readonly ObservableCollection<CartItem> _items =
        new();

    public static CartService Instance =>
        _instance;

    public ObservableCollection<CartItem> Items =>
        _items;

    public decimal Total =>
        _items.Sum(
            item => item.TotalPrice);

    public event Action? CartChanged;

    #region Add

    public void AddToCart(
        Dish dish)
    {
        CartItem? existingItem =
            _items.FirstOrDefault(
                item =>
                    item.Dish?.Id ==
                    dish.Id);

        if (existingItem != null)
        {
            existingItem.Quantity++;

            NotifyCartChanged();

            return;
        }

        CartItem item =
            new()
            {
                Dish = dish,
                Quantity = 1
            };

        _items.Add(item);

        NotifyCartChanged();
    }

    public void AddMenuToCart(Menu menu)
    {
        CartItem? existingItem =
            _items.FirstOrDefault(
                item =>
                    item.Menu?.Id ==
                    menu.Id);

        if (existingItem != null)
        {
            existingItem.Quantity++;

            NotifyCartChanged();

            return;
        }

        CartItem item =
            new()
            {
                Menu = menu,
                Quantity = 1
            };

        _items.Add(item);

        NotifyCartChanged();
    }

    #endregion

    #region Remove

    public void RemoveFromCart(
        CartItem item)
    {
        _items.Remove(item);

        NotifyCartChanged();
    }

    #endregion

    #region Quantity

    public void IncreaseQuantity(
        CartItem item)
    {
        item.Quantity++;

        NotifyCartChanged();
    }

    public void DecreaseQuantity(
        CartItem item)
    {
        item.Quantity--;

        if (item.Quantity <= 0)
        {
            _items.Remove(item);
        }

        NotifyCartChanged();
    }

    #endregion

    #region Clear

    public void ClearCart()
    {
        _items.Clear();

        NotifyCartChanged();
    }

    #endregion

    #region Events

    private void NotifyCartChanged()
    {
        CartChanged?.Invoke();
    }

    #endregion
}