namespace Order.API.Models.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }
    }
}
