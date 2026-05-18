using System.Windows;
using System.Windows.Controls;
using ViewModels;

namespace RestaurantApp.Views.Auth;

public partial class RegisterView
    : Window
{
    private readonly RegisterViewModel _viewModel;

    public RegisterView()
    {
        InitializeComponent();

        _viewModel =
            new RegisterViewModel();

        DataContext =
            _viewModel;
    }

    #region Events

    private void PasswordBox_PasswordChanged(
        object sender,
        RoutedEventArgs e)
    {
        _viewModel.Password =
            ((PasswordBox)sender).Password;
    }

    private void ConfirmPasswordBox_PasswordChanged(
        object sender,
        RoutedEventArgs e)
    {
        _viewModel.ConfirmPassword =
            ((PasswordBox)sender).Password;
    }

    #endregion
}