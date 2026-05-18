using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
using System.Configuration;

namespace BusinessLogicLayer.Services;

public class OrderService
{
    #region Fields

    private readonly OrderRepository
        _orderRepository =
            new();

    #endregion

    #region Place Order

    public void PlaceOrder(
        int userId,
        IEnumerable<CartItem> cartItems)
    {
        using RestaurantDbContext context =
            new();

        using var transaction =
            context.Database.BeginTransaction();

        try
        {
            decimal foodCost =
                cartItems.Sum(
                    item => item.TotalPrice);

            decimal minimumFreeDelivery =
                decimal.Parse(
                    ConfigurationManager.AppSettings[
                        "MinimumOrderForFreeDelivery"]!);

            decimal deliveryFeeValue =
                decimal.Parse(
                    ConfigurationManager.AppSettings[
                        "DeliveryFee"]!);

            decimal minimumOrderForDiscount =
                decimal.Parse(
                    ConfigurationManager.AppSettings[
                        "MinimumOrderForDiscount"]!);

            decimal orderDiscountPercent =
                decimal.Parse(
                    ConfigurationManager.AppSettings[
                        "OrderDiscountPercent"]!);

            decimal deliveryFee =
                foodCost >= minimumFreeDelivery
                    ? 0
                    : deliveryFeeValue;

            decimal discountAmount =
                foodCost >= minimumOrderForDiscount
                    ? foodCost *
                      (orderDiscountPercent / 100)
                    : 0;

            decimal totalCost =
                foodCost +
                deliveryFee -
                discountAmount;

            DateTime romaniaTime =
                DateTime.UtcNow.AddHours(3);

            Order order =
                new()
                {
                    OrderCode =
                        $"ORD-{Guid.NewGuid()
                            .ToString()
                            .Substring(0, 8)
                            .ToUpper()}",

                    CreatedAt =
                        romaniaTime,

                    FoodCost =
                        Math.Round(foodCost, 2),

                    DeliveryFee =
                        Math.Round(deliveryFee, 2),

                    DiscountAmount =
                        Math.Round(discountAmount, 2),

                    TotalCost =
                        Math.Round(totalCost, 2),

                    EstimatedDeliveryTime =
                        romaniaTime.AddMinutes(45),

                    Status =
                        OrderStatus.Registered,

                    UserId =
                        userId
                };

            context.Orders.Add(order);

            context.SaveChanges();

            foreach (CartItem cartItem in cartItems)
            {
                if (cartItem.Dish != null)
                {
                    Dish dish =
                        context.Dishes.First(
                            dish =>
                                dish.Id ==
                                cartItem.Dish.Id);

                    double requiredQuantity =
                        dish.PortionQuantity *
                        cartItem.Quantity;

                    if (dish.TotalQuantity <
                        requiredQuantity)
                    {
                        throw new Exception(
                            $"{dish.Name} is unavailable.");
                    }

                    OrderItem orderItem =
                        new()
                        {
                            OrderId =
                                order.Id,

                            DishId =
                                dish.Id,

                            Quantity =
                                cartItem.Quantity,

                            Price =
                                Math.Round(dish.Price, 2)
                        };

                    context.OrderItems.Add(
                        orderItem);

                    dish.TotalQuantity -=
                        requiredQuantity;

                    if (dish.TotalQuantity <
                        dish.PortionQuantity)
                    {
                        dish.IsAvailable = false;
                    }
                }
                else if (cartItem.Menu != null)
                {
                    Menu menu =
                        context.Menus
                            .Where(
                                menu =>
                                    menu.Id ==
                                    cartItem.Menu!.Id)
                            .Include(menu => menu.MenuDishes)
                                .ThenInclude(menuDish => menuDish.Dish)
                            .First();

                    decimal total =
                        menu.MenuDishes.Sum(
                            menuDish =>
                                menuDish.Dish.Price);

                    decimal discountedPrice =
                        total -
                        (total *
                         menu.DiscountPercent / 100);

                    OrderItem orderItem =
                        new()
                        {
                            OrderId =
                                order.Id,

                            MenuId =
                                menu.Id,

                            Quantity =
                                cartItem.Quantity,

                            Price =
                                Math.Round(
                                    discountedPrice,
                                    2)
                        };

                    context.OrderItems.Add(
                        orderItem);

                    foreach (MenuDish menuDish
                             in menu.MenuDishes)
                    {
                        Dish dish =
                            context.Dishes.First(
                                dish =>
                                    dish.Id ==
                                    menuDish.DishId);

                        double requiredQuantity =
                            dish.PortionQuantity *
                            cartItem.Quantity;

                        if (dish.TotalQuantity <
                            requiredQuantity)
                        {
                            throw new Exception(
                                $"{dish.Name} is unavailable.");
                        }

                        dish.TotalQuantity -=
                            requiredQuantity;

                        if (dish.TotalQuantity <
                            dish.PortionQuantity)
                        {
                            dish.IsAvailable = false;
                        }
                    }
                }
            }

            context.SaveChanges();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();

            throw;
        }
    }

    #endregion

    #region Orders

    public List<Order> GetUserOrders(
        int userId)
    {
        return _orderRepository
            .GetOrdersByUser(userId);
    }

    public List<Order> GetActiveOrders(
        int userId)
    {
        return _orderRepository
            .GetActiveOrdersByUser(userId);
    }

    public List<Order> GetAllOrders()
    {
        return _orderRepository
            .GetAllOrders();
    }

    public List<Order> GetAllActiveOrders()
    {
        return _orderRepository
            .GetAllActiveOrders();
    }

    #endregion

    #region Status

    public bool CancelOrder(
        int orderId,
        int userId)
    {
        return _orderRepository
            .CancelOrder(
                orderId,
                userId);
    }

    public void UpdateStatus(
        int orderId,
        OrderStatus status)
    {
        _orderRepository
            .UpdateStatus(
                orderId,
                status);
    }

    #endregion
}