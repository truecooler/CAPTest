using DotNetCore.CAP;
using System.Text;

namespace Worker2
{
    public class Worker : BackgroundService, ICapSubscribe
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        [CapSubscribe("test")]
        public void CheckReceivedMessage(int a)
        {
            Console.WriteLine("Consume: " + a);
        }
    }
}