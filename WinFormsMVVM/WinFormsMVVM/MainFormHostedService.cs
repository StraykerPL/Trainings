using Microsoft.Extensions.Hosting;

namespace WinFormsMVVM
{
    public sealed class MainFormHostedService : IHostedService
    {
        private readonly MainView _mainView;

        public MainFormHostedService(MainView mainView)
        {
            _mainView = mainView;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Application.Run(_mainView.MainForm);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _mainView.MainForm.Close();
            _mainView?.MainForm.Dispose();

            return Task.CompletedTask;
        }
    }
}