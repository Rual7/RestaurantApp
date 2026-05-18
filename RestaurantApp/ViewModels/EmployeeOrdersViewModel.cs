// EmployeeOrdersViewModel.cs

using BusinessLogicLayer.Services;
using Models;
using Models.Enums;
using RestaurantApp.Views.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels;

public class EmployeeOrdersViewModel
    : BaseViewModel
{
    private readonly OrderService _orderService;

    public EmployeeOrdersViewModel()
    {
        _orderService =
            new OrderService();

        UpdateStatusCommand =
            new RelayCommand(
                UpdateStatus);

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
    // Statuses
    // =====================================================

    public List<OrderStatus> Statuses =>
    [
        OrderStatus.Registered,
        OrderStatus.Preparing,
        OrderStatus.OnTheWay,
        OrderStatus.Delivered,
        OrderStatus.Cancelled
    ];

    // =====================================================
    // Commands
    // =====================================================

    public ICommand UpdateStatusCommand
    {
        get;
    }

    // =====================================================
    // Load Orders
    // =====================================================

    private void LoadOrders()
    {
        List<Order> orders =
            ShowOnlyActive
                ? _orderService.GetAllActiveOrders()
                : _orderService.GetAllOrders();

        Orders =
            new ObservableCollection<Order>(
                orders);
    }

    // =====================================================
    // Update Status
    // =====================================================

    private void UpdateStatus(
        object? parameter)
    {
        if (parameter is not Order order)
        {
            return;
        }

        _orderService.UpdateStatus(
            order.Id,
            order.Status);

        CustomMessageBox.Show(
            "Orders",
            "Order status updated.");

        LoadOrders();
    }
}