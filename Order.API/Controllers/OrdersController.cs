using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.Models.Entities;
using Order.API.ViewModels;
using Shared.Events;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly OrderAPIDbContext _context;
        readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(OrderAPIDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderVM createOrder)
        {
            Order.API.Models.Entities.Order order = new()
            {
                OrderId = Guid.NewGuid(),
                BuyerId = createOrder.BuyerId,
                CreatedDate = DateTime.Now,
                OrderStatu = Models.Enums.OrderStatus.Suspend
            };

            order.OrderItems = createOrder.OrderItems.Select(oi => new OrderItem
            {
                Count = oi.Count,
                Price = oi.Price,
                ProductId = oi.ProductId,
            }).ToList();

            order.TotalPrice = createOrder.OrderItems.Sum(oi => (oi.Price * oi.Count));

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            OrderCreatedEvent orderCreatedEvent = new()
            {
                BuyerId = order.BuyerId,
                OrderId = order.OrderId,
                OrderItems = order.OrderItems.Select(oi => new Shared.Messages.OrderItemMessage
                {
                    Count = oi.Count,
                    ProductId = oi.ProductId,
                }).ToList(),
                TotalPrice = order.TotalPrice
            };

            await _publishEndpoint.Publish(orderCreatedEvent);

            return Ok();
        }
    }
}
