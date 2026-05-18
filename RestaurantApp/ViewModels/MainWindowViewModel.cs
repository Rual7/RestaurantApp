// MainWindowViewModel.cs

using BusinessLogicLayer.Helpers;
using Models.Enums;
using RestaurantApp.Views.Auth;
using RestaurantApp.Views.Customer;
using RestaurantApp.Views.Employee;
using RestaurantApp.Views.Shared;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ViewModels;

public class MainWindowViewModel
    : BaseViewModel
{
    public MainWindowViewModel()
    {
        OpenLoginCommand =
            new RelayCommand(
                _ => OpenLogin());

        OpenRegisterCommand =
            new RelayCommand(
                _ => OpenRegister());

        LogoutCommand =
            new RelayCommand(
                _ => Logout());

        OpenOrdersCommand =
            new RelayCommand(
                _ => OpenOrders());

        OpenEmployeeOrdersCommand =
            new RelayCommand(
                _ => OpenEmployeeOrders());

        SessionManager.AuthenticationChanged +=
            RefreshUI;
    }

    // =====================================================
    // AUTH
    // =====================================================

    public bool IsAuthenticated =>
        SessionManager.IsAuthenticated;

    public bool IsGuest =>
        !SessionManager.IsAuthenticated;

    public bool IsClient =>
        SessionManager.CurrentUser?.Role ==
        UserRole.Client;

    public bool IsEmployee =>
        SessionManager.CurrentUser?.Role ==
        UserRole.Employee;

    // =====================================================
    // WELCOME
    // =====================================================

    public string WelcomeText
    {
        get
        {
            if (!SessionManager.IsAuthenticated)
            {
                return "Welcome, Guest";
            }

            return
                $"Welcome, {SessionManager.CurrentUser!.FirstName}";
        }
    }

    // =====================================================
    // MAIN CONTENT
    // =====================================================

    public object MainContentView
    {
        get
        {
            if (IsEmployee)
            {
                return new EmployeeView();
            }

            return new MenuView();
        }
    }

    // =====================================================
    // VISIBILITY
    // =====================================================

    public Visibility GuestButtonsVisibility =>
        IsGuest
            ? Visibility.Visible
            : Visibility.Collapsed;

    public Visibility LogoutButtonVisibility =>
        IsAuthenticated
            ? Visibility.Visible
            : Visibility.Collapsed;

    // =====================================================
    // CART
    // =====================================================

    public GridLength CartColumnWidth =>
        IsClient
            ? new GridLength(1, GridUnitType.Star)
            : new GridLength(0);

    // =====================================================
    // COMMANDS
    // =====================================================

    public ICommand OpenLoginCommand
    {
        get;
    }

    public ICommand OpenRegisterCommand
    {
        get;
    }

    public ICommand LogoutCommand
    {
        get;
    }

    public ICommand OpenOrdersCommand
    {
        get;
    }

    public ICommand OpenEmployeeOrdersCommand
    {
        get;
    }

    // =====================================================
    // LOGIN
    // =====================================================

    private void OpenLogin()
    {
        LoginView loginView =
            new();

        loginView.ShowDialog();

        RefreshUI();
    }

    // =====================================================
    // REGISTER
    // =====================================================

    private void OpenRegister()
    {
        RegisterView registerView =
            new();

        registerView.ShowDialog();

        RefreshUI();
    }

    // =====================================================
    // LOGOUT
    // =====================================================

    private void Logout()
    {
        SessionManager.Logout();

        RefreshUI();
    }

    // =====================================================
    // CLIENT ORDERS
    // =====================================================

    private void OpenOrders()
    {
        Window window =
            new()
            {
                Title =
                    "My Orders",

                Content =
                    new OrdersView(),

                Width =
                    420,

                Height =
                    560,

                ResizeMode =
                    ResizeMode.NoResize,

                WindowStartupLocation =
                    WindowStartupLocation.CenterScreen,

                Background =
                    Brushes.White
            };

        window.ShowDialog();
    }

    // =====================================================
    // EMPLOYEE ORDERS
    // =====================================================

    private void OpenEmployeeOrders()
    {
        Window window =
            new()
            {
                Title =
                    "Orders Management",

                Content =
                    new EmployeeOrdersView(),

                Width =
                    420,

                Height =
                    560,

                ResizeMode =
                    ResizeMode.NoResize,

                WindowStartupLocation =
                    WindowStartupLocation.CenterScreen,

                Background =
                    Brushes.White
            };

        window.ShowDialog();
    }

    // =====================================================
    // REFRESH
    // =====================================================

    private void RefreshUI()
    {
        OnPropertyChanged(nameof(IsAuthenticated));

        OnPropertyChanged(nameof(IsGuest));

        OnPropertyChanged(nameof(IsClient));

        OnPropertyChanged(nameof(IsEmployee));

        OnPropertyChanged(nameof(WelcomeText));

        OnPropertyChanged(nameof(MainContentView));

        OnPropertyChanged(nameof(GuestButtonsVisibility));

        OnPropertyChanged(nameof(LogoutButtonVisibility));

        OnPropertyChanged(nameof(CartColumnWidth));
    }
}