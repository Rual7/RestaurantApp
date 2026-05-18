using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Models.Enums;

namespace Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(150)]
    public string Email { get; set; }

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }

    [Required]
    [MaxLength(250)]
    public string Address { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public UserRole Role { get; set; }

    public ICollection<Order> Orders { get; set; }
        = new List<Order>();
}