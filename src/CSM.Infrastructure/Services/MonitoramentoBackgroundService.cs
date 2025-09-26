using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CSM.Infrastructure.Services
{
    public class MonitoramentoBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public MonitoramentoBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var monitor = scope.ServiceProvider.GetRequiredService<MonitoramentoService>();
                await monitor.VerificarAlertasAsync();

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
