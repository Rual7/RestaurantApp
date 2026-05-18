using BusinessLogicLayer.Helpers;
using DataAccessLayer.Context;
using Models;
using Models.Enums;

namespace DataAccessLayer.Seed;

public static class DbSeeder
{
    public static void Seed()
    {
        using RestaurantDbContext context = new();

        SeedEmployee(context);

        SeedCategories(context);

        SeedAllergens(context);

        SeedDishes(context);

        SeedDishAllergens(context);
    }

    // =========================================================
    // Employee
    // =========================================================

    private static void SeedEmployee(
        RestaurantDbContext context)
    {
        bool employeeExists = context.Users.Any(
            user => user.Email == "employee@restaurant.com");

        if (employeeExists)
        {
            return;
        }

        User employee = new()
        {
            FirstName = "Test Employee",
            LastName = "Test Employee",
            Email = "employee@restaurant.com",
            PhoneNumber = "0712345678",
            Address = "Restaurant HQ",
            PasswordHash =
                PasswordHelper.HashPassword("test123"),

            Role = UserRole.Employee
        };

        context.Users.Add(employee);

        context.SaveChanges();
    }

    // =========================================================
    // Categories
    // =========================================================

    private static void SeedCategories(
        RestaurantDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        List<Category> categories =
        [
            new Category
            {
                Name = "Pizza"
            },

            new Category
            {
                Name = "Burgeri"
            },

            new Category
            {
                Name = "Paste"
            },

            new Category
            {
                Name = "Desert"
            },

            new Category
            {
                Name = "Băuturi"
            }
        ];

        context.Categories.AddRange(categories);

        context.SaveChanges();
    }

    // =========================================================
    // Allergens
    // =========================================================

    private static void SeedAllergens(
        RestaurantDbContext context)
    {
        if (context.Allergens.Any())
        {
            return;
        }

        List<Allergen> allergens =
        [
            new Allergen
            {
                Name = "Gluten"
            },

            new Allergen
            {
                Name = "Lactoză"
            },

            new Allergen
            {
                Name = "Ouă"
            },

            new Allergen
            {
                Name = "Arahide"
            }
        ];

        context.Allergens.AddRange(allergens);

        context.SaveChanges();
    }

    // =========================================================
    // Dishes
    // =========================================================

    private static void SeedDishes(
    RestaurantDbContext context)
    {
        if (context.Dishes.Any())
        {
            return;
        }

        Category pizzaCategory =
            context.Categories.First(
                category => category.Name == "Pizza");

        Category burgerCategory =
            context.Categories.First(
                category => category.Name == "Burgeri");

        Category pastaCategory =
            context.Categories.First(
                category => category.Name == "Paste");

        Category dessertCategory =
            context.Categories.First(
                category => category.Name == "Desert");

        Category drinksCategory =
            context.Categories.First(
                category => category.Name == "Băuturi");

        List<Dish> dishes =
        [
            new Dish
        {
            Name = "Pizza Quattro Formaggi",

            Price = 39.99m,

            PortionQuantity = 450,

            TotalQuantity = 9000,

            Unit = "g",

            IsAvailable = true,

            CategoryId = pizzaCategory.Id
        },

        new Dish
        {
            Name = "Pizza Diavola",

            Price = 37.99m,

            PortionQuantity = 430,

            TotalQuantity = 8600,

            Unit = "g",

            IsAvailable = true,

            CategoryId = pizzaCategory.Id
        },

        new Dish
        {
            Name = "Burger Clasic",

            Price = 34.99m,

            PortionQuantity = 400,

            TotalQuantity = 6000,

            Unit = "g",

            IsAvailable = true,

            CategoryId = burgerCategory.Id
        },

        new Dish
        {
            Name = "Burger Crispy",

            Price = 35.99m,

            PortionQuantity = 420,

            TotalQuantity = 5000,

            Unit = "g",

            IsAvailable = true,

            CategoryId = burgerCategory.Id
        },

        new Dish
        {
            Name = "Paste Carbonara",

            Price = 36.99m,

            PortionQuantity = 350,

            TotalQuantity = 3,

            Unit = "g",

            IsAvailable = true,

            CategoryId = pastaCategory.Id
        },

        new Dish
        {
            Name = "Papanași",

            Price = 26.99m,

            PortionQuantity = 300,

            TotalQuantity = 4500,

            Unit = "g",

            IsAvailable = true,

            CategoryId = dessertCategory.Id
        },

        new Dish
        {
            Name = "Lava Cake",

            Price = 24.99m,

            PortionQuantity = 250,

            TotalQuantity = 3000,

            Unit = "g",

            IsAvailable = true,

            CategoryId = dessertCategory.Id
        },

        new Dish
        {
            Name = "Limonadă",

            Price = 14.99m,

            PortionQuantity = 500,

            TotalQuantity = 15000,

            Unit = "ml",

            IsAvailable = true,

            CategoryId = drinksCategory.Id
        },

        new Dish
        {
            Name = "Supă de Legume",
            Price = 24.99m,
            PortionQuantity = 350,
            TotalQuantity = 2,
            Unit = "g",
            CategoryId = 2
        },

        new Dish
        {
            Name = "Macaroane cu brânză",
            Price = 49.99m,
            PortionQuantity = 450,
            TotalQuantity = 1,
            Unit = "g",
            CategoryId = 3
        },

        ];

        context.Dishes.AddRange(dishes);

        context.SaveChanges();
    }

    // =========================================================
    // Dishes - Allergens
    // =========================================================

    private static void SeedDishAllergens(
    RestaurantDbContext context)
    {
        if (context.DishAllergens.Any())
        {
            return;
        }

        Allergen gluten =
            context.Allergens.First(
                allergen => allergen.Name == "Gluten");

        Allergen lactose =
            context.Allergens.First(
                allergen => allergen.Name == "Lactoză");

        Allergen eggs =
            context.Allergens.First(
                allergen => allergen.Name == "Ouă");

        Dish quattro =
            context.Dishes.First(
                dish => dish.Name == "Pizza Quattro Formaggi");

        Dish diavola =
            context.Dishes.First(
                dish => dish.Name == "Pizza Diavola");

        Dish burgerClasic =
            context.Dishes.First(
                dish => dish.Name == "Burger Clasic");

        Dish burgerCrispy =
            context.Dishes.First(
                dish => dish.Name == "Burger Crispy");

        Dish carbonara =
            context.Dishes.First(
                dish => dish.Name == "Paste Carbonara");

        Dish papanasi =
            context.Dishes.First(
                dish => dish.Name == "Papanași");

        Dish lavaCake =
            context.Dishes.First(
                dish => dish.Name == "Lava Cake");

        List<DishAllergen> dishAllergens =
        [
            // Pizza Quattro Formaggi

            new DishAllergen
        {
            DishId = quattro.Id,
            AllergenId = gluten.Id
        },

        new DishAllergen
        {
            DishId = quattro.Id,
            AllergenId = lactose.Id
        },

        // Pizza Diavola

        new DishAllergen
        {
            DishId = diavola.Id,
            AllergenId = gluten.Id
        },

        // Burger Clasic

        new DishAllergen
        {
            DishId = burgerClasic.Id,
            AllergenId = gluten.Id
        },

        new DishAllergen
        {
            DishId = burgerClasic.Id,
            AllergenId = lactose.Id
        },

        // Burger Crispy

        new DishAllergen
        {
            DishId = burgerCrispy.Id,
            AllergenId = gluten.Id
        },

        // Carbonara

        new DishAllergen
        {
            DishId = carbonara.Id,
            AllergenId = gluten.Id
        },

        new DishAllergen
        {
            DishId = carbonara.Id,
            AllergenId = lactose.Id
        },

        new DishAllergen
        {
            DishId = carbonara.Id,
            AllergenId = eggs.Id
        },

        // Papanași

        new DishAllergen
        {
            DishId = papanasi.Id,
            AllergenId = gluten.Id
        },

        new DishAllergen
        {
            DishId = papanasi.Id,
            AllergenId = lactose.Id
        },

        new DishAllergen
        {
            DishId = papanasi.Id,
            AllergenId = eggs.Id
        },

        // Lava Cake

        new DishAllergen
        {
            DishId = lavaCake.Id,
            AllergenId = gluten.Id
        },

        new DishAllergen
        {
            DishId = lavaCake.Id,
            AllergenId = eggs.Id
        }
        ];

        context.DishAllergens.AddRange(
            dishAllergens);

        context.SaveChanges();
    }
}