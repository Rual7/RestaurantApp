using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Index(nameof(Name), IsUnique = true)]
public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<Dish> Dishes { get; set; }
        = new List<Dish>();

    public ICollection<Menu> Menus { get; set; }
        = new List<Menu>();
}