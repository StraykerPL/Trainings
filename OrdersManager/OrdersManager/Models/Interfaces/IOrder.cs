namespace OrdersManager.Models.Interfaces
{
    public interface IOrder : IBaseEntity
    {
        string OrderingAddress { get; set; }

        OrderStatuses OrderStatus { get; set; }

        DateTime OrderDate { get; }

        ICollection<IProduct> OrderedProducts { get; }

        IOrderConfirmation? FinalizeOrder(ICollection<IProduct> products);
    }
}