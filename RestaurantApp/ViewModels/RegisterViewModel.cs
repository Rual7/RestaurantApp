using BusinessLogicLayer.Services;
using RestaurantApp.Views.Shared;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ViewModels;

public class RegisterViewModel
    : BaseViewModel
{
    private readonly AuthService _authService =
        new();

    private string _firstName =
        string.Empty;

    private string _lastName =
        string.Empty;

    private string _email =
        string.Empty;

    private string _phoneNumber =
        string.Empty;

    private string _address =
        string.Empty;

    private string _password =
        string.Empty;

    private string _confirmPassword =
        string.Empty;

    private string _firstNameError =
        string.Empty;

    private string _lastNameError =
        string.Empty;

    private string _emailError =
        string.Empty;

    private string _phoneNumberError =
        string.Empty;

    private string _addressError =
        string.Empty;

    private string _passwordError =
        string.Empty;

    private string _confirmPasswordError =
        string.Empty;

    public RegisterViewModel()
    {
        RegisterCommand =
            new RelayCommand(
                _ => Register());
    }

    #region Properties

    public string FirstName
    {
        get => _firstName;

        set
        {
            if (SetProperty(
                    ref _firstName,
                    value))
            {
                FirstNameError =
                    string.Empty;
            }
        }
    }

    public string LastName
    {
        get => _lastName;

        set
        {
            if (SetProperty(
                    ref _lastName,
                    value))
            {
                LastNameError =
                    string.Empty;
            }
        }
    }

    public string Email
    {
        get => _email;

        set
        {
            if (SetProperty(
                    ref _email,
                    value))
            {
                EmailError =
                    string.Empty;
            }
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;

        set
        {
            if (SetProperty(
                    ref _phoneNumber,
                    value))
            {
                PhoneNumberError =
                    string.Empty;
            }
        }
    }

    public string Address
    {
        get => _address;

        set
        {
            if (SetProperty(
                    ref _address,
                    value))
            {
                AddressError =
                    string.Empty;
            }
        }
    }

    public string Password
    {
        get => _password;

        set
        {
            if (SetProperty(
                    ref _password,
                    value))
            {
                PasswordError =
                    string.Empty;
            }
        }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;

        set
        {
            if (SetProperty(
                    ref _confirmPassword,
                    value))
            {
                ConfirmPasswordError =
                    string.Empty;
            }
        }
    }

    #endregion

    #region Errors

    public string FirstNameError
    {
        get => _firstNameError;

        set => SetProperty(
            ref _firstNameError,
            value);
    }

    public string LastNameError
    {
        get => _lastNameError;

        set => SetProperty(
            ref _lastNameError,
            value);
    }

    public string EmailError
    {
        get => _emailError;

        set => SetProperty(
            ref _emailError,
            value);
    }

    public string PhoneNumberError
    {
        get => _phoneNumberError;

        set => SetProperty(
            ref _phoneNumberError,
            value);
    }

    public string AddressError
    {
        get => _addressError;

        set => SetProperty(
            ref _addressError,
            value);
    }

    public string PasswordError
    {
        get => _passwordError;

        set => SetProperty(
            ref _passwordError,
            value);
    }

    public string ConfirmPasswordError
    {
        get => _confirmPasswordError;

        set => SetProperty(
            ref _confirmPasswordError,
            value);
    }

    #endregion

    #region Commands

    public ICommand RegisterCommand
    {
        get;
    }

    #endregion

    #region Register

    private void Register()
    {
        bool isValid =
            true;

        if (string.IsNullOrWhiteSpace(
                FirstName))
        {
            FirstNameError =
                "First name is required.";

            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(
                LastName))
        {
            LastNameError =
                "Last name is required.";

            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(
                Email))
        {
            EmailError =
                "Email is required.";

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

        if (string.IsNullOrWhiteSpace(
                PhoneNumber))
        {
            PhoneNumberError =
                "Phone number is required.";

            isValid = false;
        }
        else if (!Regex.IsMatch(
                     PhoneNumber,
                     @"^\d{10,15}$"))
        {
            PhoneNumberError =
                "Phone number must contain 10-15 digits.";

            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(
                Address))
        {
            AddressError =
                "Address is required.";

            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(
                Password))
        {
            PasswordError =
                "Password is required.";

            isValid = false;
        }
        else if (Password.Length < 6)
        {
            PasswordError =
                "Password must contain at least 6 characters.";

            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(
                ConfirmPassword))
        {
            ConfirmPasswordError =
                "Please confirm your password.";

            isValid = false;
        }
        else if (Password !=
                 ConfirmPassword)
        {
            ConfirmPasswordError =
                "Passwords do not match.";

            isValid = false;
        }

        if (!isValid)
        {
            return;
        }

        bool success =
            _authService.Register(
                FirstName,
                LastName,
                Email,
                PhoneNumber,
                Address,
                Password);

        if (success)
        {
            CustomMessageBox.Show(
                "Success",
                "Account created successfully!");
        }
        else
        {
            EmailError =
                "Email already exists.";
        }
    }

    #endregion
}