using System.Windows.Controls;
using ViewModels;

namespace RestaurantApp.Views.Employee;

public partial class EmployeeView
    : UserControl
{
    public EmployeeView()
    {
        InitializeComponent();

        DataContext =
            new EmployeeViewModel();
    }
}