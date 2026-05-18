using System.Windows.Controls;
using ViewModels;

namespace RestaurantApp.Views.Employee;

public partial class EmployeeOrdersView
    : UserControl
{
    public EmployeeOrdersView()
    {
        InitializeComponent();

        DataContext =
            new EmployeeOrdersViewModel();
    }
}