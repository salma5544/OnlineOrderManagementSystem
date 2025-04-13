using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Data;
using OnlineOrderManagementSystem.DTOs;
using OnlineOrderManagementSystem.Models;

namespace OnlineOrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrderManagementDbContext _context;

        public OrdersController(OrderManagementDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var customer = await _context.Customers.FindAsync(dto.CustomerId);
            if (customer == null) return NotFound("Customer not found");

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
            };

            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null) return NotFound($"Product ID {item.ProductId} not found");
                if (product.StockQuantity < item.Quantity) return BadRequest($"Insufficient stock for {product.Name}");

                var subtotal = item.Quantity * product.Price;
                product.StockQuantity -= item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Subtotal = subtotal
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { order.Id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound("Order not found");

            var result = new OrderDetailsDto
            {
                Id = order.Id,
                Status = order.Status,
                OrderDate = order.OrderDate,
                Customer = order.Customer,
                Items = order.OrderItems.Select(i => new OrderItemDetailsDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Subtotal = i.Subtotal
                }).ToList()
            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = dto.Status;
            order.StatusUpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Order status updated to {order.Status}." });

        }
    }
}
