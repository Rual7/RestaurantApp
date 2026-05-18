using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Services;
using Models;
using RestaurantApp.Views.Shared;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ViewModels;

public class CartViewModel : BaseViewModel
{
    private readonly CartService _cartService;

    private readonly OrderService _orderService;

    public CartViewModel()
    {
        _cartService =
            CartService.Instance;

        _orderService =
            new OrderService();

        _cartService.CartChanged +=
            Refresh;

        // =====================================================
        // Commands
        // =====================================================

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

    // =====================================================
    // Properties
    // =====================================================

    public ObservableCollection<CartItem> Items =>
        _cartService.Items;

    public decimal Total =>
        _cartService.Total;

    public bool IsEmpty =>
        !Items.Any();

    // =====================================================
    // Commands
    // =====================================================

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

    // =====================================================
    // Quantity
    // =====================================================

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

    // =====================================================
    // Remove
    // =====================================================

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

    // =====================================================
    // Clear
    // =====================================================

    private void ClearCart()
    {
        _cartService.ClearCart();
    }

    // =====================================================
    // Place Order
    // =====================================================

    // CartViewModel.cs
    // ÎNLOCUIEȘTE doar metoda PlaceOrder()

    private void PlaceOrder()
    {
        // =====================================================
        // Empty Cart
        // =====================================================

        if (IsEmpty)
        {
            CustomMessageBox.Show(
                "Cart",
                "Your cart is empty.");

            return;
        }

        // =====================================================
        // Authentication
        // =====================================================

        if (!SessionManager.IsAuthenticated)
        {
            CustomMessageBox.Show(
                "Authentication",
                "You must be logged in.");

            return;
        }

        try
        {
            // =================================================
            // Place Order
            // =================================================

            _orderService.PlaceOrder(
                SessionManager.CurrentUser!.Id,
                Items);

            // =================================================
            // Clear Cart
            // =================================================

            _cartService.ClearCart();

            // =================================================
            // Success
            // =================================================

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

    // =====================================================
    // Refresh
    // =====================================================

    private void Refresh()
    {
        OnPropertyChanged(nameof(Items));
        OnPropertyChanged(nameof(Total));
        OnPropertyChanged(nameof(IsEmpty));
    }
}