using OrdersManager.Models.Interfaces;

namespace OrdersManager.Models
{
    public class Product : IProduct
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; }
    }
}