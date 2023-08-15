namespace Prodavnica.Api.Models
{
    public class Oreder
    {
        public Guid OrderId { get; set; }
        public Guid ByerId { get; set; }
        public Guid ShoppingItemId { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public List<ShoppingItem> Items { get; set; }
        public bool Finalized { get; set; }

    }
}
