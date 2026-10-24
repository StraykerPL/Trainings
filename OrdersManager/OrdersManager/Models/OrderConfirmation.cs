using OrdersManager.Models.Interfaces;
using OrdersManager.Providers;
using OrdersManager.UserInterface.Interfaces;
using System.Text.Json;

namespace OrdersManager.Models
{
    public class OrderConfirmation : IOrderConfirmation
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();

        public IOrder AssignedOrder { get; private set; }

        public double TotalValue { get; private set; }

        private readonly IOutputProvider _outputProvider;

        public OrderConfirmation(
            IOrder assignedOrder,
            IOutputProvider outputProvider)
        {
            AssignedOrder = assignedOrder;
            _outputProvider = outputProvider;
            CalculateTotalValue();
        }

        public void DisplayConfirmationInfo()
        {
            _outputProvider.Output(JsonSerializer.Serialize(AssignedOrder, JsonSerializerOptionsProvider.GetOptionsObject()));
        }

        private void CalculateTotalValue()
        {
            double totalValue = 0.0;

            foreach (var order in AssignedOrder.OrderedProducts)
            {
                totalValue += order.Price;
            }

            TotalValue = totalValue;
        }
    }
}