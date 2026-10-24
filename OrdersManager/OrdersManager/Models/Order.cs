using OrdersManager.Constants;
using OrdersManager.Models.Interfaces;
using OrdersManager.UserInterface.Interfaces;
using System.Timers;
using Timer = System.Timers.Timer;

namespace OrdersManager.Models
{
    public class Order : IOrder
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();

        public string OrderingAddress { get; set; } = string.Empty;

        public OrderStatuses OrderStatus { get; set; } = OrderStatuses.Ordering;

        public DateTime OrderDate { get; private set; } = DateTime.MinValue;

        public ICollection<IProduct> OrderedProducts { get; private set; } = [];

        private readonly IOutputProvider _outputProvider;
        private readonly Timer _timer;
        private const double OrderCompletionMilisecondsTiming = 300000;

        public Order(IOutputProvider outputProvider)
        {
            _outputProvider = outputProvider;
            _timer = new Timer(OrderCompletionMilisecondsTiming)
            {
                AutoReset = false
            };
            _timer.Elapsed += OnTimePassed;
        }

        public IOrderConfirmation? FinalizeOrder(ICollection<IProduct> products)
        {
            if (OrderingAddress is null or "")
            {
                return null;
            }

            if (products is null || products.Count is 0)
            {
                return null;
            }

            foreach (var product in products)
            {
                OrderedProducts.Add(product);
            }

            OrderDate = DateTime.UtcNow;
            OrderStatus = OrderStatuses.Packaging;
            _outputProvider.OutputLine(MessagesConstants.OrderFinalizationSuccessMessage);
            _timer.Start();

            return new OrderConfirmation(this, _outputProvider);
        }

        private void OnTimePassed(object? source, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
            OrderStatus = OrderStatuses.Sended;
        }
    }
}