using Models.Enums;
using RestaurantApp.Models;

namespace Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public ICollection<Order> Orders { get; set; }
        = new List<Order>();
}