using System.Windows;
using ViewModels;

namespace RestaurantApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();
    }
}