using System.Windows.Controls;
using ViewModels;

namespace RestaurantApp.Views.Customer;

public partial class CartView : UserControl
{
    public CartView()
    {
        InitializeComponent();

        DataContext =
            new CartViewModel();
    }
}