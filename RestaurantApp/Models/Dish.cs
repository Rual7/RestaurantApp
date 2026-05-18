using System.ComponentModel.DataAnnotations;

namespace Models;

public class Dish
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Range(0.01, 100000)]
    public decimal Price { get; set; }

    [Range(1, 10000)]
    public double PortionQuantity { get; set; }

    [Range(0, 100000)]
    public double TotalQuantity { get; set; }

    [Required]
    [MaxLength(20)]
    public string Unit { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int CategoryId { get; set; }

    public Category Category { get; set; }

    public ICollection<DishAllergen> DishAllergens { get; set; }
        = new List<DishAllergen>();

    public ICollection<MenuDish> MenuDishes { get; set; }
        = new List<MenuDish>();

    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}