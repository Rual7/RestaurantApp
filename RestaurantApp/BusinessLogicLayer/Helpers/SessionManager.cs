using Models;

namespace BusinessLogicLayer.Helpers;

public static class SessionManager
{
    public static User? CurrentUser { get; private set; }

    public static bool IsLoggedIn =>
        CurrentUser != null;

    public static bool IsEmployee =>
        CurrentUser?.Role == Models.Enums.UserRole.Employee;

    public static void Login(User user)
    {
        CurrentUser = user;
    }

    public static void Logout()
    {
        CurrentUser = null;
    }
}