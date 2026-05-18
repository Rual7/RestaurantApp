using BusinessLogicLayer.Helpers;
using DataAccessLayer.Context;
using Models;
using Models.Enums;

namespace DataAccessLayer.Seed;

public static class DbSeeder
{
    public static void Seed()
    {
        using RestaurantDbContext context = new();

        bool employeeExists = context.Users.Any(
            user => user.Email == "employee@restaurant.com");

        if (!employeeExists)
        {
            User employee = new()
            {
                FirstName = "Test Employee",
                LastName = "Test Employee",
                Email = "employee@restaurant.com",
                PhoneNumber = "0712345678",
                Address = "Restaurant HQ",
                PasswordHash =
                    PasswordHelper.HashPassword("test123"),

                Role = UserRole.Employee
            };

            context.Users.Add(employee);

            context.SaveChanges();
        }
    }
}