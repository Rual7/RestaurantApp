using RestaurantApp.Models;

namespace Models;

public class Dish
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public double PortionQuantity { get; set; }

    public double TotalQuantity { get; set; }

    public string Unit { get; set; }

    public string ImagePath { get; set; }

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