using OrdersManager.Constants;
using OrdersManager.Models;
using OrdersManager.Providers;
using OrdersManager.Services;
using OrdersManager.Services.Interfaces;
using OrdersManager.UserInterface;
using OrdersManager.UserInterface.Interfaces;
using System.Text.Json;

namespace OrdersManager
{
    internal class Program
    {
        private static readonly IOutputProvider _outputProvider = new ConsoleOutputProvider();
        private static readonly IInputProvider _inputProvider = new ConsoleInputProvider();
        private static readonly IBasketService _basketService = new BasicBasketService();
        private static readonly IOrderService _orderService = new BasicOrderService(_outputProvider);

        private static void CreateProduct()
        {
            var newProduct = new Product();

            _outputProvider.OutputLine(MessagesConstants.AddProductNameMessage);
            newProduct.Name = _inputProvider.GetInput();
            _outputProvider.OutputLine(MessagesConstants.AddProductDescriptionMessage);
            newProduct.Description = _inputProvider.GetInput();
            _outputProvider.OutputLine(MessagesConstants.AddProductPriceMessage);

            if (newProduct.Name is "" || newProduct.Description is "")
            {
                _outputProvider.OutputLine(MessagesConstants.NoDataProvidedForProductMessage);

                return;
            }

            try
            {
                newProduct.Price = Convert.ToDouble(_inputProvider.GetInput());
            }
            catch (Exception)
            {
                _outputProvider.OutputLine(MessagesConstants.DoubleConvertionErrorMessage);

                return;
            }

            _basketService.AddProductToBasket(newProduct);
        }

        private static void DeleteProduct()
        {
            _outputProvider.OutputLine(MessagesConstants.ProvideProductIdMessage);
            var requestedId = _inputProvider.GetInput();

            if (requestedId is "")
            {
                _outputProvider.OutputLine(MessagesConstants.NoIdProvidedMessage);
            }

            _basketService.RemoveProductFromBasket(requestedId);
        }

        private static void ShowBasket()
        {
            if (_basketService.Products.Count is 0)
            {
                _outputProvider.OutputLine(MessagesConstants.BasketEmptyMessage);
            }

            foreach (var productInBasket in _basketService.Products)
            {
                _outputProvider.OutputLine(JsonSerializer.Serialize(productInBasket, JsonSerializerOptionsProvider.GetOptionsObject()));
            }

            _outputProvider.OutputLine(string.Format(MessagesConstants.BasketValueMessage, _basketService.CalculateBasketValue()));
        }

        private static void MakeOrder()
        {
            _outputProvider.OutputLine(MessagesConstants.ProvideShippingAddressMessage);
            string address = _inputProvider.GetInput();

            if (address is "")
            {
                _outputProvider.OutputLine(MessagesConstants.NoDeliveryAddressProvidedMessage);

                return;
            }

            if (_orderService.CreateNewOrder(address, _basketService.Products))
            {
                _basketService.Products.Clear();
            }
        }

        private static void Main()
        {
            _outputProvider.OutputLine(MessagesConstants.WelcomeMessage);

            string userInput;
            while (true)
            {
                _outputProvider.Output(MessagesConstants.OptionsMessage);

                userInput = _inputProvider.GetInput();
                switch (userInput)
                {
                    case "1":
                        CreateProduct();
                        break;

                    case "2":
                        DeleteProduct();
                        break;

                    case "3":
                        ShowBasket();
                        break;

                    case "4":
                        MakeOrder();
                        break;

                    case "5":
                        _orderService.DisplayOrders();

                        break;

                    case "6":
                        return;

                    default:
                        _outputProvider.OutputLine(MessagesConstants.WrongImputDataMessage);

                        break;
                }
            }
        }
    }
}