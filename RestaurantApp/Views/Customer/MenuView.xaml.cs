using System.Windows.Controls;
using ViewModels;

namespace RestaurantApp.Views.Customer;

public partial class MenuView
    : UserControl
{
    public MenuView()
    {
        InitializeComponent();

        DataContext =
            new MenuViewModel();
    }
}