using OrdersManager.UserInterface.Interfaces;

namespace OrdersManager.UserInterface
{
    internal sealed class ConsoleOutputProvider : IOutputProvider
    {
        public void Output(string outputData)
        {
            Console.Write(outputData);
        }

        public void OutputLine(string outputData)
        {
            Console.WriteLine(outputData);
        }
    }
}