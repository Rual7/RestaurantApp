// OrdersViewModel.cs

using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Services;
using Models;
using Models.Enums;
using RestaurantApp.Views.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels;

public class OrdersViewModel : BaseViewModel
{
    private readonly OrderService _orderService;

    public OrdersViewModel()
    {
        _orderService =
            new OrderService();

        CancelOrderCommand =
            new RelayCommand(
                CancelOrder);

        LoadOrders();
    }

    // =====================================================
    // Orders
    // =====================================================

    private ObservableCollection<Order> _orders =
        [];

    public ObservableCollection<Order> Orders
    {
        get => _orders;
        set => SetProperty(ref _orders, value);
    }

    // =====================================================
    // Active Filter
    // =====================================================

    private bool _showOnlyActive;

    public bool ShowOnlyActive
    {
        get => _showOnlyActive;
        set
        {
            if (SetProperty(
                    ref _showOnlyActive,
                    value))
            {
                LoadOrders();
            }
        }
    }

    // =====================================================
    // Commands
    // =====================================================

    public ICommand CancelOrderCommand
    {
        get;
    }

    // =====================================================
    // Load
    // =====================================================

    private void LoadOrders()
    {
        if (!SessionManager.IsAuthenticated ||
            SessionManager.CurrentUser == null)
        {
            return;
        }

        List<Order> orders =
            ShowOnlyActive
                ? _orderService.GetActiveOrders(
                    SessionManager.CurrentUser.Id)
                : _orderService.GetUserOrders(
                    SessionManager.CurrentUser.Id);

        Orders =
            new ObservableCollection<Order>(
                orders);
    }

    // =====================================================
    // Cancel
    // =====================================================

    private void CancelOrder(
        object? parameter)
    {
        if (parameter is not Order order)
        {
            return;
        }

        bool success =
            _orderService.CancelOrder(
                order.Id,
                SessionManager.CurrentUser!.Id);

        if (!success)
        {
            CustomMessageBox.Show(
                "Order",
                "Order cannot be cancelled.");

            return;
        }

        order.Status =
            OrderStatus.Cancelled;

        LoadOrders();

        CustomMessageBox.Show(
            "Order",
            "Order cancelled successfully.");
    }
}