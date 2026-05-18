using System.Windows;
using ViewModels;

namespace RestaurantApp;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        _viewModel = new MainWindowViewModel();

        DataContext = _viewModel;
    }
}