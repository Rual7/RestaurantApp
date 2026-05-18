// MainWindowViewModel.cs

using BusinessLogicLayer.Helpers;
using RestaurantApp.Views.Auth;
using RestaurantApp.Views.Customer;
using RestaurantApp.Views.Shared;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel()
    {
        OpenLoginCommand =
            new RelayCommand(
                _ => OpenLogin());

        OpenRegisterCommand =
            new RelayCommand(
                _ => OpenRegister());

        OpenOrdersCommand =
            new RelayCommand(
                _ => OpenOrders());

        LogoutCommand =
            new RelayCommand(
                _ => Logout());

        SessionManager.AuthenticationChanged +=
            RefreshUI;

        RefreshUI();
    }

    // =====================================================
    // Welcome Text
    // =====================================================

    private string _welcomeText =
        "Welcome, Guest";

    public string WelcomeText
    {
        get => _welcomeText;
        set => SetProperty(
            ref _welcomeText,
            value);
    }

    // =====================================================
    // Visibility
    // =====================================================

    private Visibility _guestButtonsVisibility;

    public Visibility GuestButtonsVisibility
    {
        get => _guestButtonsVisibility;
        set => SetProperty(
            ref _guestButtonsVisibility,
            value);
    }

    private Visibility _logoutButtonVisibility;

    public Visibility LogoutButtonVisibility
    {
        get => _logoutButtonVisibility;
        set => SetProperty(
            ref _logoutButtonVisibility,
            value);
    }

    // =====================================================
    // Client
    // =====================================================

    private bool _isClient;

    public bool IsClient
    {
        get => _isClient;
        set => SetProperty(
            ref _isClient,
            value);
    }

    // =====================================================
    // Cart Column
    // =====================================================

    private GridLength _cartColumnWidth =
        new(0);

    public GridLength CartColumnWidth
    {
        get => _cartColumnWidth;
        set => SetProperty(
            ref _cartColumnWidth,
            value);
    }

    // =====================================================
    // Commands
    // =====================================================

    public ICommand OpenLoginCommand
    {
        get;
    }

    public ICommand OpenRegisterCommand
    {
        get;
    }

    public ICommand OpenOrdersCommand
    {
        get;
    }

    public ICommand LogoutCommand
    {
        get;
    }

    // =====================================================
    // UI
    // =====================================================

    public void RefreshUI()
    {
        IsClient =
            SessionManager.IsClient;

        CartColumnWidth =
            IsClient
                ? new GridLength(
                    1.2,
                    GridUnitType.Star)
                : new GridLength(0);

        if (!SessionManager.IsAuthenticated)
        {
            WelcomeText =
                "Welcome, Guest";

            GuestButtonsVisibility =
                Visibility.Visible;

            LogoutButtonVisibility =
                Visibility.Collapsed;

            return;
        }

        WelcomeText =
            $"Welcome, {SessionManager.CurrentUser!.FirstName}";

        GuestButtonsVisibility =
            Visibility.Collapsed;

        LogoutButtonVisibility =
            Visibility.Visible;
    }

    // =====================================================
    // Auth
    // =====================================================

    private void OpenLogin()
    {
        LoginView loginView =
            new();

        loginView.ShowDialog();
    }

    private void OpenRegister()
    {
        RegisterView registerView =
            new();

        registerView.ShowDialog();
    }

    private void OpenOrders()
    {
        Window window =
            new()
            {
                Title = "My Orders",

                Height = 560,

                Width = 420,

                ResizeMode =
                    ResizeMode.NoResize,

                WindowStartupLocation =
                    WindowStartupLocation.CenterScreen,

                Background =
                    new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString(
                            "#F5F5F5")),

                Content =
                    new OrdersView()
            };

        window.ShowDialog();
    }

    private void Logout()
    {
        SessionManager.Logout();

        CustomMessageBox.Show(
            "Logout",
            "You have been logged out successfully.");
    }
}