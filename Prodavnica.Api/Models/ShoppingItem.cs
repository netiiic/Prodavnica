namespace Prodavnica.Api.Models
{
    public class ShoppingItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid SellerId { get; set; }
        public List<Oreder> Order { get; set; }
        public bool Bought { get; set; }
    }
}
