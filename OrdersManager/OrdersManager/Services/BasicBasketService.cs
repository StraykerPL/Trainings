using OrdersManager.Models.Interfaces;
using OrdersManager.Services.Interfaces;

namespace OrdersManager.Services
{
    public sealed class BasicBasketService : IBasketService
    {
        public ICollection<IProduct> Products { get; private set; } = [];

        public void AddProductToBasket(IProduct productToAdd)
        {
            if (productToAdd is null || Products.Contains(productToAdd))
            {
                return;
            }

            Products.Add(productToAdd);
        }

        public void RemoveProductFromBasket(string productId)
        {
            foreach (var product in Products)
            {
                if (product.Id == productId)
                {
                    Products.Remove(product);

                    return;
                }
            }
        }

        public double CalculateBasketValue()
        {
            var totalValue = 0.0;

            foreach (var product in Products)
            {
                totalValue += product.Price;
            }

            return totalValue;
        }
    }
}