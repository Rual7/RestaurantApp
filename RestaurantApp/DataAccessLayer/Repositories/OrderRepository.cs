// OrderRepository.cs

using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;

namespace DataAccessLayer.Repositories;

public class OrderRepository
{
    // =====================================================
    // User Orders
    // =====================================================

    public List<Order> GetOrdersByUser(
        int userId)
    {
        using RestaurantDbContext context =
            new();

        return context.Orders
            .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Dish)
            .Where(order => order.UserId == userId)
            .OrderByDescending(order => order.CreatedAt)
            .ToList();
    }

    // =====================================================
    // Active User Orders
    // =====================================================

    public List<Order> GetActiveOrdersByUser(
        int userId)
    {
        using RestaurantDbContext context =
            new();

        return context.Orders
            .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Dish)
            .Where(order =>
                order.UserId == userId &&
                order.Status != OrderStatus.Delivered &&
                order.Status != OrderStatus.Cancelled)
            .OrderByDescending(order => order.CreatedAt)
            .ToList();
    }

    // =====================================================
    // All Orders
    // =====================================================

    public List<Order> GetAllOrders()
    {
        using RestaurantDbContext context =
            new();

        return context.Orders
            .Include(order => order.User)
            .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Dish)
            .OrderByDescending(order => order.CreatedAt)
            .ToList();
    }

    // =====================================================
    // Active Orders
    // =====================================================

    public List<Order> GetAllActiveOrders()
    {
        using RestaurantDbContext context =
            new();

        return context.Orders
            .Include(order => order.User)
            .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Dish)
            .Where(order =>
                order.Status != OrderStatus.Delivered &&
                order.Status != OrderStatus.Cancelled)
            .OrderByDescending(order => order.CreatedAt)
            .ToList();
    }

    // =====================================================
    // Cancel Order
    // =====================================================

    public bool CancelOrder(
        int orderId,
        int userId)
    {
        using RestaurantDbContext context =
            new();

        Order? order =
            context.Orders.FirstOrDefault(
                order =>
                    order.Id == orderId &&
                    order.UserId == userId);

        if (order == null)
        {
            return false;
        }

        if (order.Status ==
            OrderStatus.Delivered)
        {
            return false;
        }

        order.Status =
            OrderStatus.Cancelled;

        context.SaveChanges();

        return true;
    }

    // =====================================================
    // Update Status
    // =====================================================

    public void UpdateStatus(
        int orderId,
        OrderStatus status)
    {
        using RestaurantDbContext context =
            new();

        Order? order =
            context.Orders.FirstOrDefault(
                order => order.Id == orderId);

        if (order == null)
        {
            return;
        }

        order.Status = status;

        context.SaveChanges();
    }
}