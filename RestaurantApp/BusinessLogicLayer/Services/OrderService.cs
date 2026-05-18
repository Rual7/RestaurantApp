// OrderService.cs

using BusinessLogicLayer.Helpers;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Models;
using Models.Enums;
using System.Configuration;

namespace BusinessLogicLayer.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;

    public OrderService()
    {
        _orderRepository =
            new OrderRepository();
    }

    // =====================================================
    // Place Order
    // =====================================================

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
            // =================================================
            // Costs
            // =================================================

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

            // =================================================
            // Delivery
            // =================================================

            decimal deliveryFee =
                foodCost >= minimumFreeDelivery
                    ? 0
                    : deliveryFeeValue;

            // =================================================
            // Discount
            // =================================================

            decimal discountAmount =
                foodCost >= minimumOrderForDiscount
                    ? foodCost *
                      (orderDiscountPercent / 100)
                    : 0;

            // =================================================
            // Total
            // =================================================

            decimal totalCost =
                foodCost +
                deliveryFee -
                discountAmount;

            // =================================================
            // Romania Time
            // =================================================

            DateTime romaniaTime =
                DateTime.UtcNow.AddHours(3);

            // =================================================
            // Create Order
            // =================================================

            Order order =
                new()
                {
                    OrderCode =
                        GenerateOrderCode(),

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

            // =================================================
            // Items
            // =================================================

            foreach (CartItem cartItem in cartItems)
            {
                Dish dish =
                    context.Dishes.First(
                        dish =>
                            dish.Id ==
                            cartItem.Dish.Id);

                // stock validation

                double requiredQuantity =
                    dish.PortionQuantity *
                    cartItem.Quantity;

                if (dish.TotalQuantity <
                    requiredQuantity)
                {
                    throw new Exception(
                        $"{dish.Name} is unavailable.");
                }

                // create order item

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

                // update stock

                dish.TotalQuantity -=
                    requiredQuantity;

                if (dish.TotalQuantity <
                    dish.PortionQuantity)
                {
                    dish.IsAvailable = false;
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

    // =====================================================
    // Get Orders
    // =====================================================

    public List<Order> GetUserOrders(
        int userId)
    {
        return _orderRepository.GetOrdersByUser(
            userId);
    }

    // =====================================================
    // Get Active Orders
    // =====================================================

    public List<Order> GetActiveOrders(
        int userId)
    {
        return _orderRepository.GetActiveOrdersByUser(
            userId);
    }

    // =====================================================
    // Cancel Order
    // =====================================================

    public bool CancelOrder(
        int orderId,
        int userId)
    {
        return _orderRepository.CancelOrder(
            orderId,
            userId);
    }

    // =====================================================
    // Helpers
    // =====================================================

    private string GenerateOrderCode()
    {
        return
            $"ORD-{Guid.NewGuid()
                .ToString()
                .Substring(0, 8)
                .ToUpper()}";
    }


    // =====================================================
    // Employee Orders
    // =====================================================

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

    // =====================================================
    // Update Status
    // =====================================================

    public void UpdateStatus(
        int orderId,
        OrderStatus status)
    {
        _orderRepository.UpdateStatus(
            orderId,
            status);
    }
}