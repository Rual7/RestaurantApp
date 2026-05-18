using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Services;
using RestaurantApp.Views.Shared;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly AuthService _authService;

    public LoginViewModel()
    {
        _authService = new AuthService();

        LoginCommand = new RelayCommand(_ => Login());
    }

    // =========================
    // Properties
    // =========================

    private string _email = string.Empty;

    public string Email
    {
        get => _email;
        set
        {
            if (SetProperty(ref _email, value))
            {
                EmailError = string.Empty;
            }
        }
    }

    private string _password = string.Empty;

    public string Password
    {
        get => _password;
        set
        {
            if (SetProperty(ref _password, value))
            {
                PasswordError = string.Empty;
            }
        }
    }

    // =========================
    // Error Properties
    // =========================

    private string _emailError = string.Empty;

    public string EmailError
    {
        get => _emailError;
        set => SetProperty(ref _emailError, value);
    }

    private string _passwordError = string.Empty;

    public string PasswordError
    {
        get => _passwordError;
        set => SetProperty(ref _passwordError, value);
    }

    // =========================
    // Commands
    // =========================

    public ICommand LoginCommand { get; }

    // =========================
    // Methods
    // =========================

    private void Login()
    {
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(Email))
        {
            EmailError = "Email is required.";
            isValid = false;
        }
        else if (!Regex.IsMatch(
                     Email,
                     @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            EmailError =
                "Invalid email format. Example: name@example.com";

            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            PasswordError = "Password is required.";
            isValid = false;
        }

        if (!isValid)
        {
            return;
        }

        var user = _authService.Login(
            Email,
            Password);

        if (user == null)
        {
            CustomMessageBox.Show(
                "Login Failed",
                "Invalid email or password.");

            return;
        }

        SessionManager.Login(user);

        CustomMessageBox.Show(
            "Success",
            $"Welcome back, {user.FirstName}!");

        Application.Current.Windows
            .OfType<Window>()
            .SingleOrDefault(window => window.IsActive)?
            .Close();
    }
}