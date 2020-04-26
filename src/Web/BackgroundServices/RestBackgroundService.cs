using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web.BackgroundServices
{
    public class RestBackgroundService : BackgroundService
    {
        private ISenderService senderService;
        private IApplicationLogger<SenderService> logger;
        private readonly IServiceScopeFactory services;
        private readonly DelayBackgroundServices delay;

        public RestBackgroundService(IServiceScopeFactory services, DelayBackgroundServices delay)
        {
            this.services = services;
            this.delay = delay;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (delay.CanStart)
                {
                    using (var scope = services.CreateScope())
                    {
                        InitializeScope(scope.ServiceProvider);
                        senderService.FetchAddress();
                        await SendOldNotSentSchedules();
                        await Task.Delay(1000 * 60, stoppingToken);
                    }
                }
                else
                {
                    await Task.Delay(1000 * 10, stoppingToken);
                }

            }
        }

        private void InitializeScope(IServiceProvider serviceProvider)
        {
            senderService = serviceProvider.GetRequiredService<ISenderService>();
            logger = serviceProvider.GetRequiredService<IApplicationLogger<SenderService>>();
        }

        private async Task SendOldNotSentSchedules()
        {
            var now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 1, DateTimeKind.Local);
            foreach (var schedule in senderService.OldNotSentSchedules(now))
            {
                try
                {
                    if (!schedule.ArrivalSent && schedule.Arrival <= now)
                    {
                        await senderService.SendArrival(schedule);
                    }
                    if (!schedule.DepartureSent && schedule.Departure <= now)
                    {
                        await senderService.SendDeparture(schedule);
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e);
                }
            }
        }
    }
}
