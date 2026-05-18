// OrderRepository.cs

using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;

namespace DataAccessLayer.Repositories;

public class OrderRepository
{
    // =====================================================
    // Get All Orders By User
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
    // Get Active Orders
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
                o =>
                    o.Id == orderId &&
                    o.UserId == userId);

        if (order == null)
        {
            return false;
        }

        // already finished

        if (order.Status == OrderStatus.Delivered ||
            order.Status == OrderStatus.Cancelled)
        {
            return false;
        }

        order.Status =
            OrderStatus.Cancelled;

        context.SaveChanges();

        return true;
    }
}