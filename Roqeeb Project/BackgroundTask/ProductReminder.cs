using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Service;

namespace Roqeeb_Project.BackgroundTask
{
    public class ProductReminder : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<ProductReminder> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ProductReminder(IServiceScopeFactory serviceScopeFactory, ILogger<ProductReminder> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var productContext = scope.ServiceProvider.GetRequiredService<IProductService>();
                    var products = await productContext.ViewAllProducts(stoppingToken);
                    var lowProductContext = scope.ServiceProvider.GetRequiredService<ILowProduct>();
                    foreach (var product in products.Data)
                    {
                        var myProduct = await productContext.GetProduct(product.Id, stoppingToken);
                        var newProduct = new Product
                        {
                            ProductName = myProduct.Data.ProductName,
                            Quantity = myProduct.Data.Quantity
                        };
                        if(product.Quantity == product.Quantity)
                        {
                           await lowProductContext.LowProductMessage(newProduct, stoppingToken);
                        }
                    }
                }
                catch(Exception e)
                {
                    _logger.LogError($"Error occured while implementing reminder. {e.Message}");
                    _logger.LogError(e, e.Message);
                }
                _logger.LogInformation($"Background Hosted Servie for {nameof(ProductReminder)} is stopping");
                var timeSpan = _nextRun - now;
                await Task.Delay(timeSpan, stoppingToken);
                _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow.AddHours(3));
            }
        }
    }
}
