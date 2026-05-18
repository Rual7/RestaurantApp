using System.ComponentModel.DataAnnotations;

namespace Models;

public class MenuDish
{
    public int MenuId
    {
        get;
        set;
    }

    public Menu Menu
    {
        get;
        set;
    }

    public int DishId
    {
        get;
        set;
    }

    public Dish Dish
    {
        get;
        set;
    }

    [Range(1, 10000)]
    public double Quantity
    {
        get;
        set;
    }
}