using System.Windows;
using System.Windows.Controls;
using ViewModels;

namespace RestaurantApp.Views.Auth;

public partial class LoginView
    : Window
{
    private readonly LoginViewModel _viewModel;

    public LoginView()
    {
        InitializeComponent();

        _viewModel =
            new LoginViewModel();

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

    #endregion
}