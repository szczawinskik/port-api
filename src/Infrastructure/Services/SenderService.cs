using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Messages;
using Infrastructure.Interfaces;
using Infrastructure.Wrappers.HttpClientWrapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SenderService: ISenderService
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IHttpClientWrapper httpClient;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IApplicationLogger<SenderService> logger;
        private string remoteAddress = string.Empty;

        public SenderService(IScheduleRepository scheduleRepository, IConfigurationRepository configurationRepository,
            IHttpClientWrapper httpClient)
        {
            this.scheduleRepository = scheduleRepository;
            this.httpClient = httpClient;
            this.configurationRepository = configurationRepository;
        }

        public void FetchAddress()
        {
            remoteAddress = configurationRepository.GetConfigurationValue(ConfigurationType.RemoteServiceAddress);
        }

        public IQueryable<Schedule> OldNotSentSchedules(DateTime now)
        {
            return scheduleRepository.GetAllWithShips()
                .Where(x => x.Arrival <= now || x.Departure <= now
                && x.ArrivalSent == false || x.DepartureSent == false);
        }

        public async Task SendArrival(Schedule schedule)
        {
            var message = new ArrivalMessage
            {
                ShipName = schedule.Ship.Name
            };
            await SendMessage(schedule, message);
        }

        public async Task SendDeparture(Schedule schedule)
        {
            var message = new DepartureMessage
            {
                ShipName = schedule.Ship.Name
            };
            await SendMessage(schedule, message);
        }

        private async Task SendMessage(Schedule schedule, MessageBase message)
        {
            var response = await httpClient.PostMessage(remoteAddress, message);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                schedule.DepartureSent = true;
                scheduleRepository.Update(schedule);
            }
        }
    }
}
