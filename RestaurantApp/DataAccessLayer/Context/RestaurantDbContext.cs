using Microsoft.EntityFrameworkCore;
using Models;
using System.Configuration;

namespace DataAccessLayer.Context;

public class RestaurantDbContext
    : DbContext
{
    public DbSet<User> Users
    {
        get;
        set;
    }

    public DbSet<Category> Categories
    {
        get;
        set;
    }

    public DbSet<Dish> Dishes
    {
        get;
        set;
    }

    public DbSet<Menu> Menus
    {
        get;
        set;
    }

    public DbSet<Allergen> Allergens
    {
        get;
        set;
    }

    public DbSet<DishAllergen> DishAllergens
    {
        get;
        set;
    }

    public DbSet<MenuDish> MenuDishes
    {
        get;
        set;
    }

    public DbSet<Order> Orders
    {
        get;
        set;
    }

    public DbSet<OrderItem> OrderItems
    {
        get;
        set;
    }

    #region Configuration

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString =
                ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

            optionsBuilder.UseNpgsql(
                connectionString);
        }
    }

    #endregion

    #region Model Creating

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DishAllergen>()
            .HasKey(
                dishAllergen =>
                    new
                    {
                        dishAllergen.DishId,
                        dishAllergen.AllergenId
                    });

        modelBuilder.Entity<MenuDish>()
            .HasKey(
                menuDish =>
                    new
                    {
                        menuDish.MenuId,
                        menuDish.DishId
                    });

        modelBuilder.Entity<OrderItem>()
            .Property(
                orderItem => orderItem.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(
                order => order.FoodCost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(
                order => order.DeliveryFee)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(
                order => order.DiscountAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(
                order => order.TotalCost)
            .HasColumnType("decimal(18,2)");
    }

    #endregion
}