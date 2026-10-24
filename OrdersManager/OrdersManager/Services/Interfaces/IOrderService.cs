using OrdersManager.Models.Interfaces;

namespace OrdersManager.Services.Interfaces
{
    public interface IOrderService
    {
        ICollection<IOrderConfirmation> OrdersConfirmations { get; }

        bool CreateNewOrder(string address, ICollection<IProduct> products);

        void DisplayOrders();
    }
}