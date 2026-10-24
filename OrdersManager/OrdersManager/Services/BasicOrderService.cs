using OrdersManager.Constants;
using OrdersManager.Models;
using OrdersManager.Models.Interfaces;
using OrdersManager.Providers;
using OrdersManager.Services.Interfaces;
using OrdersManager.UserInterface.Interfaces;
using System.Text.Json;

namespace OrdersManager.Services
{
    public sealed class BasicOrderService : IOrderService
    {
        public ICollection<IOrderConfirmation> OrdersConfirmations { get; } = [];

        private readonly IOutputProvider _outputProvider;

        public BasicOrderService(IOutputProvider provider)
        {
            _outputProvider = provider;
        }

        public bool CreateNewOrder(string address, ICollection<IProduct> products)
        {
            if (products.Count is 0)
            {
                _outputProvider.OutputLine(MessagesConstants.NoProductsInBasketForOrderingMessage);

                return false;
            }

            if (address is "")
            {
                _outputProvider.OutputLine(MessagesConstants.NoDeliveryAddressProvidedMessage);

                return false;
            }

            var newOrder = new Order(_outputProvider)
            {
                OrderingAddress = address
            };
            var confirmation = newOrder.FinalizeOrder(products);

            if (confirmation is not null)
            {
                OrdersConfirmations.Add(confirmation);

                return true;
            }
            else
            {
                _outputProvider.OutputLine(MessagesConstants.OrderFinalizationErrorMessage);

                return false;
            }
        }

        public void DisplayOrders()
        {
            if (OrdersConfirmations.Count is 0)
            {
                _outputProvider.OutputLine(MessagesConstants.NoOrdersToDisplayMessage);

                return;
            }

            foreach (var orderConfirm in OrdersConfirmations)
            {
                _outputProvider.OutputLine(JsonSerializer.Serialize(orderConfirm, JsonSerializerOptionsProvider.GetOptionsObject()));
            }
        }
    }
}