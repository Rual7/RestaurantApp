using System.ComponentModel.DataAnnotations;

namespace Models;

public class OrderItem
{
    public int Id
    {
        get;
        set;
    }

    [Range(1, 1000)]
    public int Quantity
    {
        get;
        set;
    }

    [Range(0.01, 100000)]
    public decimal Price
    {
        get;
        set;
    }

    public int OrderId
    {
        get;
        set;
    }

    public Order Order
    {
        get;
        set;
    }

    public int? DishId
    {
        get;
        set;
    }

    public Dish? Dish
    {
        get;
        set;
    }

    public int? MenuId
    {
        get;
        set;
    }

    public Menu? Menu
    {
        get;
        set;
    }
}