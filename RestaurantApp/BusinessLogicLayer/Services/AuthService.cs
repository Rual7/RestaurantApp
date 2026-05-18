using BusinessLogicLayer.Helpers;
using DataAccessLayer.Context;
using Models;
using Models.Enums;

namespace BusinessLogicLayer.Services;

public class AuthService
{
    #region Register

    public bool Register(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string address,
        string password)
    {
        using RestaurantDbContext context =
            new();

        bool userExists =
            context.Users.Any(
                user => user.Email == email);

        if (userExists)
        {
            return false;
        }

        User user =
            new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                PasswordHash =
                    PasswordHelper.HashPassword(password),
                Role = UserRole.Client
            };

        context.Users.Add(user);

        context.SaveChanges();

        return true;
    }

    #endregion

    #region Login

    public User? Login(
        string email,
        string password)
    {
        using RestaurantDbContext context =
            new();

        string hashedPassword =
            PasswordHelper.HashPassword(password);

        return context.Users.FirstOrDefault(
            user =>
                user.Email == email &&
                user.PasswordHash == hashedPassword);
    }

    #endregion
}