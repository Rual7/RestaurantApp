using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Models.Enums;

namespace Models;

[Index(nameof(OrderCode), IsUnique = true)]
public class Order
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string OrderCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal FoodCost { get; set; }

    public decimal DeliveryFee { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TotalCost { get; set; }

    public DateTime EstimatedDeliveryTime { get; set; }

    public OrderStatus Status { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}