namespace OrdersManager.Models.Interfaces
{
    public interface IProduct : IBaseEntity
    {
        string Name { get; set; }

        string Description { get; set; }

        double Price { get; set; }
    }
}