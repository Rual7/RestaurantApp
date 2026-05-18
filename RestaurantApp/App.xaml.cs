using System.Windows;
using DataAccessLayer.Seed;

namespace RestaurantApp;

public partial class App : Application
{
    public App()
    {
        DbSeeder.Seed();
    }
}