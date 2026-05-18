using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Services;
using Models;
using RestaurantApp.Views.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels;

public class CartViewModel
    : BaseViewModel
{
    private readonly CartService _cartService =
        CartService.Instance;

    private readonly OrderService _orderService =
        new();

    public CartViewModel()
    {
        _cartService.CartChanged +=
            Refresh;

        IncreaseQuantityCommand =
            new RelayCommand(
                IncreaseQuantity);

        DecreaseQuantityCommand =
            new RelayCommand(
                DecreaseQuantity);

        RemoveItemCommand =
            new RelayCommand(
                RemoveItem);

        ClearCartCommand =
            new RelayCommand(
                _ => ClearCart());

        PlaceOrderCommand =
            new RelayCommand(
                _ => PlaceOrder());

        Refresh();
    }

    #region Properties

    public ObservableCollection<CartItem> Items =>
        _cartService.Items;

    public decimal Total =>
        _cartService.Total;

    public bool IsEmpty =>
        !Items.Any();

    #endregion

    #region Commands

    public ICommand IncreaseQuantityCommand
    {
        get;
    }

    public ICommand DecreaseQuantityCommand
    {
        get;
    }

    public ICommand RemoveItemCommand
    {
        get;
    }

    public ICommand ClearCartCommand
    {
        get;
    }

    public ICommand PlaceOrderCommand
    {
        get;
    }

    #endregion

    #region Quantity

    private void IncreaseQuantity(
        object? parameter)
    {
        if (parameter is not CartItem item)
        {
            return;
        }

        _cartService.IncreaseQuantity(
            item);
    }

    private void DecreaseQuantity(
        object? parameter)
    {
        if (parameter is not CartItem item)
        {
            return;
        }

        _cartService.DecreaseQuantity(
            item);
    }

    #endregion

    #region Remove

    private void RemoveItem(
        object? parameter)
    {
        if (parameter is not CartItem item)
        {
            return;
        }

        _cartService.RemoveFromCart(
            item);
    }

    #endregion

    #region Clear

    private void ClearCart()
    {
        _cartService.ClearCart();
    }

    #endregion

    #region Place Order

    private void PlaceOrder()
    {
        if (IsEmpty)
        {
            CustomMessageBox.Show(
                "Cart",
                "Your cart is empty.");

            return;
        }

        if (!SessionManager.IsAuthenticated)
        {
            CustomMessageBox.Show(
                "Authentication",
                "You must be logged in.");

            return;
        }

        try
        {
            _orderService.PlaceOrder(
                SessionManager.CurrentUser!.Id,
                Items);

            _cartService.ClearCart();

            CustomMessageBox.Show(
                "Order",
                "Order placed successfully.");
        }
        catch
        {
            CustomMessageBox.Show(
                "Order",
                "Failed to place order.");
        }
    }

    #endregion

    #region Refresh

    private void Refresh()
    {
        OnPropertyChanged(
            nameof(Items));

        OnPropertyChanged(
            nameof(Total));

        OnPropertyChanged(
            nameof(IsEmpty));
    }

    #endregion
}