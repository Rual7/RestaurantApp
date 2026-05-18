using Models;
using Models.Enums;

namespace BusinessLogicLayer.Helpers;

public static class SessionManager
{
    public static User? CurrentUser
    {
        get;
        private set;
    }

    public static bool IsAuthenticated =>
        CurrentUser != null;

    public static bool IsClient =>
        CurrentUser?.Role ==
        UserRole.Client;

    public static bool IsEmployee =>
        CurrentUser?.Role ==
        UserRole.Employee;

    public static event Action? AuthenticationChanged;

    public static void Login(
        User user)
    {
        CurrentUser = user;

        AuthenticationChanged?.Invoke();
    }

    public static void Logout()
    {
        CurrentUser = null;

        AuthenticationChanged?.Invoke();
    }
}