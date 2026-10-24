namespace OrdersManager.Models.Interfaces
{
    public interface IOrderConfirmation : IBaseEntity
    {
        IOrder AssignedOrder { get; }

        double TotalValue { get; }

        void DisplayConfirmationInfo();
    }
}