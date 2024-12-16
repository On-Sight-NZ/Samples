using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnSight.RealtimeData.Services.Jobs
{
  public class JobHostedService : IHostedService
  {
    private readonly IScheduler _scheduler;
    private readonly IServiceProvider services;

    public JobHostedService(IScheduler scheduler, IServiceProvider services)
    {
      _scheduler = scheduler;
      this.services = services;
    }

    public Task StartAsync(CancellationToken ct)
    {
      _scheduler.JobFactory = new JobFactory(services);

      var jobDetails = JobBuilder
        .CreateForAsync<ConnectToSignalR>()
        .WithIdentity("FetchCredentialsJob")
        .WithDescription("Fetch credentials to communicate with other platforms")
        .Build();

      var trigger = TriggerBuilder
          .Create()
          .WithSchedule(SimpleScheduleBuilder.RepeatMinutelyForever())
          .StartNow()
          .Build();

      return Task.WhenAll(_scheduler.Start(ct), _scheduler.ScheduleJob(jobDetails, trigger));
    }

    public Task StopAsync(CancellationToken ct)
    {
      return _scheduler.Shutdown(ct);
    }
  }

  public class JobFactory : IJobFactory
  {
    private readonly IServiceProvider _serviceProvider;

    public JobFactory(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
      return ActivatorUtilities.CreateInstance(_serviceProvider, bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job)
    {
      if (job is IDisposable disposableJob)
        disposableJob.Dispose();
    }
  }
}
