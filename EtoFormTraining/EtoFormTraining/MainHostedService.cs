using Eto.Forms;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace EtoFormTraining
{
    public sealed class MainHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var ok = new Form();
            ok.Size = new Eto.Drawing.Size(300, 300);
            ok.Show();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}