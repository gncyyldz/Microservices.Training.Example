namespace Order.API.ViewModels
{
    public class CreateOrderVM
    {
        public Guid BuyerId { get; set; }
        public List<CreateOrderItemVM> OrderItems { get; set; }
    }
}
