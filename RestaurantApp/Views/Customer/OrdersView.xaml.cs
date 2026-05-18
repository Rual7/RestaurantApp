using System.Windows.Controls;
using ViewModels;

namespace RestaurantApp.Views.Customer;

public partial class OrdersView
    : UserControl
{
    public OrdersView()
    {
        InitializeComponent();

        DataContext =
            new OrdersViewModel();
    }
}