using System.Windows;
using ViewModels;

namespace RestaurantApp;

public partial class MainWindow
    : Window
{
    #region Constructor

    public MainWindow()
    {
        InitializeComponent();

        DataContext =
            new MainWindowViewModel();
    }

    #endregion
}