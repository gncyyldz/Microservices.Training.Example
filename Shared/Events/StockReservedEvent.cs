using Shared.Events.Common;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockReservedEvent : IEvent
    {
        public Guid BuyerId { get; set; }
        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
