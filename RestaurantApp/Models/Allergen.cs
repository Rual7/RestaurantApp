using RestaurantApp.Models;

namespace Models;

public class Allergen
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<DishAllergen> DishAllergens { get; set; }
        = new List<DishAllergen>();
}