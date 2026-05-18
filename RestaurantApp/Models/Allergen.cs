using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Models;

[Index(nameof(Name), IsUnique = true)]
public class Allergen
{
    public int Id
    {
        get;
        set;
    }

    [Required]
    [MaxLength(100)]
    public string Name
    {
        get;
        set;
    }

    public ICollection<DishAllergen> DishAllergens
    {
        get;
        set;
    }
        = new List<DishAllergen>();
}