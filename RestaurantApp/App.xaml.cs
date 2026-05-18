using System.Windows;
using DataAccessLayer.Seed;

namespace RestaurantApp;

public partial class App
    : Application
{
    #region Constructor

    public App()
    {
        DbSeeder.Seed();
    }

    #endregion
}