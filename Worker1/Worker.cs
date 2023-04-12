using DotNetCore.CAP;
using System.Text;

namespace Worker1
{
    public class Worker : BackgroundService//, ICapSubscribe
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICapPublisher _capBus;

        public Worker(ILogger<Worker> logger, ICapPublisher capBus)
        {
            _logger = logger;
            _capBus = capBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                await _capBus.PublishAsync("test", i++);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(4000, stoppingToken);
            }
        }
    }
}