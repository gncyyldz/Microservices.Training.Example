using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumers
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {
        readonly OrderAPIDbContext _orderAPIDbContext;

        public StockNotReservedEventConsumer(OrderAPIDbContext orderAPIDbContext)
        {
            _orderAPIDbContext = orderAPIDbContext;
        }

        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            Order.API.Models.Entities.Order order = await _orderAPIDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == context.Message.OrderId);
            order.OrderStatu = Models.Enums.OrderStatus.Failed;
            await _orderAPIDbContext.SaveChangesAsync();
        }
    }
}
