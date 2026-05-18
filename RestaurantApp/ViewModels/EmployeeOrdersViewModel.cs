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
    private readonly OrderService _orderService =
        new();

    private ObservableCollection<Order> _orders =
        [];

    private bool _showOnlyActive;

    public EmployeeOrdersViewModel()
    {
        UpdateStatusCommand =
            new RelayCommand(
                UpdateStatus);

        LoadOrders();
    }

    #region Properties

    public ObservableCollection<Order> Orders
    {
        get => _orders;

        set => SetProperty(
            ref _orders,
            value);
    }

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

    public List<OrderStatus> Statuses =>
    [
        OrderStatus.Registered,
        OrderStatus.Preparing,
        OrderStatus.OnTheWay,
        OrderStatus.Delivered,
        OrderStatus.Cancelled
    ];

    #endregion

    #region Commands

    public ICommand UpdateStatusCommand
    {
        get;
    }

    #endregion

    #region Load Orders

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

    #endregion

    #region Update Status

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

    #endregion
}