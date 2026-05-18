using System.ComponentModel.DataAnnotations;

namespace Models;

public class Menu
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Range(0, 100)]
    public decimal DiscountPercent { get; set; }

    [MaxLength(500)]
    public string ImagePath { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int CategoryId { get; set; }

    public Category Category { get; set; }

    public ICollection<MenuDish> MenuDishes { get; set; }
        = new List<MenuDish>();
}