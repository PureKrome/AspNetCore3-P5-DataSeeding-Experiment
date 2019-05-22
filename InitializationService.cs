using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication7
{
    public class InitializationService : IHostedService
    {
        private readonly ILogger<InitializationService> _logger;

        public InitializationService(ILogger<InitializationService> logger)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to do some data initialization.");

            // Read in the names from a file to simulate doing some 'data stuff'
            var names = await File.ReadAllLinesAsync("Names.txt", cancellationToken);

            _logger.LogInformation($"Rad in {names.Length} names from file.");


            // do something like save it to a db.
            _logger.LogInformation("Storing names into database....");

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken); // Stall for 3 seconds.

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            _logger.LogInformation(" ... done!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
