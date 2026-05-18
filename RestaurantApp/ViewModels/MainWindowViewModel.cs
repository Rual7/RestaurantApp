using BusinessLogicLayer.Helpers;
using RestaurantApp.Views.Auth;
using RestaurantApp.Views.Shared;
using System.Windows;
using System.Windows.Input;

namespace ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel()
    {
        OpenLoginCommand = new RelayCommand(_ => OpenLogin());
        OpenRegisterCommand = new RelayCommand(_ => OpenRegister());
        LogoutCommand = new RelayCommand(_ => Logout());

        RefreshUI();
    }

    private string _welcomeText = "Welcome, Guest";

    public string WelcomeText
    {
        get => _welcomeText;
        set => SetProperty(ref _welcomeText, value);
    }

    private Visibility _guestButtonsVisibility;

    public Visibility GuestButtonsVisibility
    {
        get => _guestButtonsVisibility;
        set => SetProperty(ref _guestButtonsVisibility, value);
    }

    private Visibility _logoutButtonVisibility;

    public Visibility LogoutButtonVisibility
    {
        get => _logoutButtonVisibility;
        set => SetProperty(ref _logoutButtonVisibility, value);
    }

    public ICommand OpenLoginCommand { get; }

    public ICommand OpenRegisterCommand { get; }

    public ICommand LogoutCommand { get; }

    public void RefreshUI()
    {
        if (!SessionManager.IsLoggedIn)
        {
            WelcomeText = "Welcome, Guest";

            GuestButtonsVisibility = Visibility.Visible;
            LogoutButtonVisibility = Visibility.Collapsed;

            return;
        }

        WelcomeText = $"Welcome, {SessionManager.CurrentUser!.FirstName}";

        GuestButtonsVisibility = Visibility.Collapsed;
        LogoutButtonVisibility = Visibility.Visible;
    }

    private void OpenLogin()
    {
        LoginView loginView = new();

        loginView.ShowDialog();

        RefreshUI();
    }

    private void OpenRegister()
    {
        RegisterView registerView = new();

        registerView.ShowDialog();
    }

    private void Logout()
    {
        SessionManager.Logout();

        RefreshUI();

        CustomMessageBox.Show(
            "Logout",
            "You have been logged out successfully.");
    }
}