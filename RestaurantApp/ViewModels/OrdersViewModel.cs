using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Services;
using Models;
using Models.Enums;
using RestaurantApp.Views.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels;

public class OrdersViewModel
    : BaseViewModel
{
    private readonly OrderService _orderService =
        new();

    private ObservableCollection<Order> _orders =
        [];

    private bool _showOnlyActive;

    public OrdersViewModel()
    {
        CancelOrderCommand =
            new RelayCommand(
                CancelOrder);

        LoadOrders();
    }

    #region Orders

    public ObservableCollection<Order> Orders
    {
        get => _orders;

        set => SetProperty(
            ref _orders,
            value);
    }

    #endregion

    #region Active Filter

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

    #endregion

    #region Commands

    public ICommand CancelOrderCommand
    {
        get;
    }

    #endregion

    #region Load Orders

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

    #endregion

    #region Cancel Order

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

    #endregion
}