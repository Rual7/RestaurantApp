namespace Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Dish> Dishes { get; set; }
        = new List<Dish>();

    public ICollection<Menu> Menus { get; set; }
        = new List<Menu>();
}