namespace Prodavnica.Api.Dto
{
    public class ShoppingItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid SellerId { get; set; }
        public bool Bought { get; set; }
        public bool Procesed { get; set; }
        public Guid BoughtId { get; set; }
    }
}
