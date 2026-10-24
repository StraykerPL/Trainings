using OrdersManager.Models.Interfaces;

namespace OrdersManager.Services.Interfaces
{
    public interface IBasketService
    {
        ICollection<IProduct> Products { get; }

        void AddProductToBasket(IProduct productToAdd);

        void RemoveProductFromBasket(string productId);

        double CalculateBasketValue();
    }
}