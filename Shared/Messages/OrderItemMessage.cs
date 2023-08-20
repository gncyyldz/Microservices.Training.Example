using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class OrderItemMessage
    {
        public string ProductId { get; set; }
        public int Count { get; set; }
    }
}
