namespace OrdersManager.UserInterface.Interfaces
{
    public interface IOutputProvider
    {
        void OutputLine(string outputData);

        void Output(string outputData);
    }
}