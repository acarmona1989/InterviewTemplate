using Football.Application.ExternalServices;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Football.Application.JobQuartz
{
    public class NotifyImproperAligmentJob : IJob
    {
        private IImproperAligmentNotificationService ImproperAligmentNotificationService { get; }

        public NotifyImproperAligmentJob(IImproperAligmentNotificationService improperAligmentNotificationService)
        {
            ImproperAligmentNotificationService = improperAligmentNotificationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await ImproperAligmentNotificationService.NotifyImpromerAligmentAsync();
        }
    }
}
