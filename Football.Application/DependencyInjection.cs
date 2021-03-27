using FluentValidation;
using Football.Application.Commons;
using Football.Application.Commons.Behaviours;
using Football.Application.ExternalServices;
using Football.Application.JobQuartz;
using Football.Application.Matchs.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Collections.Generic;
using System.Reflection;

namespace Football.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient<IImproperAligmentNotificationService, ImproperAligmentNotificationService>();

            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<QuartzJobRunner>();
            services.AddScoped<NotifyImproperAligmentJob>();
            services.Decorate(typeof(IRequestHandler<,>), typeof(RetryDecoratorHandler<,>));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NotifyImproperAligmentJob),
                cronExpression: "0/50 * * * * ?")); //every 10 seconds
            return services;
        }
    }
}
