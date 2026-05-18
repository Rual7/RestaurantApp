using BusinessLogicLayer.Helpers;
using DataAccessLayer.Context;
using Models;
using Models.Enums;

namespace DataAccessLayer.Seed;

public static class DbSeeder
{
    public static void Seed()
    {
        using RestaurantDbContext context =
            new();

        SeedEmployee(context);

        SeedCategories(context);

        SeedAllergens(context);

        SeedDishes(context);

        SeedDishAllergens(context);

        SeedMenus(context);

        SeedMenuDishes(context);
    }

    #region Employee

    private static void SeedEmployee(
        RestaurantDbContext context)
    {
        bool employeeExists =
            context.Users.Any(
                user =>
                    user.Email ==
                    "employee@restaurant.com");

        if (employeeExists)
        {
            return;
        }

        User employee =
            new()
            {
                FirstName = "Employee",
                LastName = "Employee",
                Email = "employee@restaurant.com",
                PhoneNumber = "0712345678",
                Address = "Restaurant HQ",
                PasswordHash =
                    PasswordHelper.HashPassword(
                        "test123"),
                Role = UserRole.Employee
            };

        context.Users.Add(employee);

        context.SaveChanges();
    }

    #endregion

    #region Categories

    private static void SeedCategories(
        RestaurantDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        List<Category> categories =
        [
            new() { Name = "Pizza" },
            new() { Name = "Burgeri" },
            new() { Name = "Paste" },
            new() { Name = "Desert" },
            new() { Name = "Băuturi" },
            new() { Name = "Supe" },
            new() { Name = "Salate" },
            new() { Name = "Mic Dejun" }
        ];

        context.Categories.AddRange(categories);

        context.SaveChanges();
    }

    #endregion

    #region Allergens

    private static void SeedAllergens(
        RestaurantDbContext context)
    {
        if (context.Allergens.Any())
        {
            return;
        }

        List<Allergen> allergens =
        [
            new() { Name = "Gluten" },
            new() { Name = "Lactoză" },
            new() { Name = "Ouă" },
            new() { Name = "Arahide" },
            new() { Name = "Soia" },
            new() { Name = "Pește" },
            new() { Name = "Țelină" }
        ];

        context.Allergens.AddRange(allergens);

        context.SaveChanges();
    }

    #endregion

    #region Dishes

    private static void SeedDishes(
        RestaurantDbContext context)
    {
        if (context.Dishes.Any())
        {
            return;
        }

        Dictionary<string, int> categories =
            context.Categories
                .ToDictionary(
                    category => category.Name,
                    category => category.Id);

        List<Dish> dishes =
        [
            // Pizza

            new()
            {
                Name = "Pizza Quattro Formaggi",
                Price = 39.99m,
                PortionQuantity = 450,
                TotalQuantity = 9000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Pizza"]
            },

            new()
            {
                Name = "Pizza Diavola",
                Price = 37.99m,
                PortionQuantity = 430,
                TotalQuantity = 8600,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Pizza"]
            },

            new()
            {
                Name = "Pizza Prosciutto",
                Price = 36.99m,
                PortionQuantity = 420,
                TotalQuantity = 5000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Pizza"]
            },

            // Burgers

            new()
            {
                Name = "Burger Clasic",
                Price = 34.99m,
                PortionQuantity = 400,
                TotalQuantity = 6000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Burgeri"]
            },

            new()
            {
                Name = "Burger Crispy",
                Price = 35.99m,
                PortionQuantity = 420,
                TotalQuantity = 5000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Burgeri"]
            },

            new()
            {
                Name = "Cheese Burger",
                Price = 38.99m,
                PortionQuantity = 430,
                TotalQuantity = 4000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Burgeri"]
            },

            // Pasta

            new()
            {
                Name = "Paste Carbonara",
                Price = 36.99m,
                PortionQuantity = 350,
                TotalQuantity = 3,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Paste"]
            },

            new()
            {
                Name = "Paste Alfredo",
                Price = 35.99m,
                PortionQuantity = 360,
                TotalQuantity = 5000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Paste"]
            },

            new()
            {
                Name = "Macaroane cu Brânză",
                Price = 32.99m,
                PortionQuantity = 400,
                TotalQuantity = 1,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Paste"]
            },

            // Dessert

            new()
            {
                Name = "Papanași",
                Price = 26.99m,
                PortionQuantity = 300,
                TotalQuantity = 4500,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Desert"]
            },

            new()
            {
                Name = "Lava Cake",
                Price = 24.99m,
                PortionQuantity = 250,
                TotalQuantity = 3000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Desert"]
            },

            new()
            {
                Name = "Cheesecake",
                Price = 27.99m,
                PortionQuantity = 280,
                TotalQuantity = 2500,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Desert"]
            },

            // Drinks

            new()
            {
                Name = "Limonadă",
                Price = 14.99m,
                PortionQuantity = 500,
                TotalQuantity = 15000,
                Unit = "ml",
                IsAvailable = true,
                CategoryId = categories["Băuturi"]
            },

            new()
            {
                Name = "Fresh Portocale",
                Price = 18.99m,
                PortionQuantity = 400,
                TotalQuantity = 8000,
                Unit = "ml",
                IsAvailable = true,
                CategoryId = categories["Băuturi"]
            },

            new()
            {
                Name = "Cappuccino",
                Price = 12.99m,
                PortionQuantity = 300,
                TotalQuantity = 9000,
                Unit = "ml",
                IsAvailable = true,
                CategoryId = categories["Băuturi"]
            },

            // Soups

            new()
            {
                Name = "Supă de Legume",
                Price = 24.99m,
                PortionQuantity = 350,
                TotalQuantity = 2,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Supe"]
            },

            new()
            {
                Name = "Ciorbă de Burtă",
                Price = 28.99m,
                PortionQuantity = 400,
                TotalQuantity = 5000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Supe"]
            },

            // Salads

            new()
            {
                Name = "Salată Caesar",
                Price = 31.99m,
                PortionQuantity = 350,
                TotalQuantity = 4000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Salate"]
            },

            new()
            {
                Name = "Salată Grecească",
                Price = 29.99m,
                PortionQuantity = 330,
                TotalQuantity = 3500,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Salate"]
            },

            // Breakfast

            new()
            {
                Name = "Omletă Țărănească",
                Price = 22.99m,
                PortionQuantity = 300,
                TotalQuantity = 3000,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Mic Dejun"]
            },

            new()
            {
                Name = "English Breakfast",
                Price = 34.99m,
                PortionQuantity = 500,
                TotalQuantity = 2500,
                Unit = "g",
                IsAvailable = true,
                CategoryId = categories["Mic Dejun"]
            }
        ];

        context.Dishes.AddRange(dishes);

        context.SaveChanges();
    }

    #endregion

    #region Dish Allergens

    private static void SeedDishAllergens(
        RestaurantDbContext context)
    {
        if (context.DishAllergens.Any())
        {
            return;
        }

        Dictionary<string, int> allergens =
            context.Allergens
                .ToDictionary(
                    allergen => allergen.Name,
                    allergen => allergen.Id);

        Dictionary<string, int> dishes =
            context.Dishes
                .ToDictionary(
                    dish => dish.Name,
                    dish => dish.Id);

        List<DishAllergen> dishAllergens =
        [
            new()
            {
                DishId = dishes["Pizza Quattro Formaggi"],
                AllergenId = allergens["Gluten"]
            },

            new()
            {
                DishId = dishes["Pizza Quattro Formaggi"],
                AllergenId = allergens["Lactoză"]
            },

            new()
            {
                DishId = dishes["Paste Carbonara"],
                AllergenId = allergens["Ouă"]
            },

            new()
            {
                DishId = dishes["Paste Carbonara"],
                AllergenId = allergens["Gluten"]
            },

            new()
            {
                DishId = dishes["Cheesecake"],
                AllergenId = allergens["Lactoză"]
            },

            new()
            {
                DishId = dishes["Cheesecake"],
                AllergenId = allergens["Ouă"]
            },

            new()
            {
                DishId = dishes["Salată Caesar"],
                AllergenId = allergens["Ouă"]
            },

            new()
            {
                DishId = dishes["Ciorbă de Burtă"],
                AllergenId = allergens["Țelină"]
            },

            new()
            {
                DishId = dishes["Macaroane cu Brânză"],
                AllergenId = allergens["Lactoză"]
            }
        ];

        context.DishAllergens.AddRange(
            dishAllergens);

        context.SaveChanges();
    }

    #endregion

    #region Menus

    private static void SeedMenus(
        RestaurantDbContext context)
    {
        if (context.Menus.Any())
        {
            return;
        }

        Dictionary<string, int> categories =
            context.Categories
                .ToDictionary(
                    category => category.Name,
                    category => category.Id);

        List<Menu> menus =
        [
            new()
        {
            Name = "Italian Combo",
            DiscountPercent = 10,
            ImagePath = string.Empty,
            IsAvailable = true,
            CategoryId = categories["Pizza"]
        },

        new()
        {
            Name = "Burger Combo",
            DiscountPercent = 15,
            ImagePath = string.Empty,
            IsAvailable = true,
            CategoryId = categories["Burgeri"]
        },

        new()
        {
            Name = "Breakfast Combo",
            DiscountPercent = 12,
            ImagePath = string.Empty,
            IsAvailable = true,
            CategoryId = categories["Mic Dejun"]
        }
        ];

        context.Menus.AddRange(menus);

        context.SaveChanges();
    }

    #endregion

    #region Menu Dishes

    private static void SeedMenuDishes(
        RestaurantDbContext context)
    {
        if (context.MenuDishes.Any())
        {
            return;
        }

        Dictionary<string, int> menus =
            context.Menus
                .ToDictionary(
                    menu => menu.Name,
                    menu => menu.Id);

        Dictionary<string, int> dishes =
            context.Dishes
                .ToDictionary(
                    dish => dish.Name,
                    dish => dish.Id);

        List<MenuDish> menuDishes =
        [
            // Italian Combo

            new()
        {
            MenuId = menus["Italian Combo"],
            DishId = dishes["Pizza Quattro Formaggi"],
            Quantity = 1
        },

        new()
        {
            MenuId = menus["Italian Combo"],
            DishId = dishes["Paste Carbonara"],
            Quantity = 1
        },

        new()
        {
            MenuId = menus["Italian Combo"],
            DishId = dishes["Limonadă"],
            Quantity = 1
        },

        // Burger Combo

        new()
        {
            MenuId = menus["Burger Combo"],
            DishId = dishes["Burger Clasic"],
            Quantity = 1
        },

        new()
        {
            MenuId = menus["Burger Combo"],
            DishId = dishes["Fresh Portocale"],
            Quantity = 1
        },

        // Breakfast Combo

        new()
        {
            MenuId = menus["Breakfast Combo"],
            DishId = dishes["Omletă Țărănească"],
            Quantity = 1
        },

        new()
        {
            MenuId = menus["Breakfast Combo"],
            DishId = dishes["Cappuccino"],
            Quantity = 1
        }
        ];

        context.MenuDishes.AddRange(menuDishes);

        context.SaveChanges();
    }

    #endregion
}