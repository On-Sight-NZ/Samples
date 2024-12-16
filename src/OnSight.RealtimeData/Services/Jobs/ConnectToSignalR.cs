using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace OnSight.RealtimeData.Services.Jobs
{
  [DisallowConcurrentExecution]
  public class ConnectToSignalR : IJob
  {
    private readonly IOnSightCoreService onsightService;
    private readonly ILogger<IOnSightCoreService> logger;
    private readonly IServiceProvider serviceProvider;

    public ConnectToSignalR(IOnSightCoreService deviceMacCache, ILogger<IOnSightCoreService> logger,
      IServiceProvider serviceProvider)
    {
      this.onsightService = deviceMacCache;
      this.logger = logger;
      this.serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      await onsightService.EnsureAllConnected(serviceProvider, logger).ConfigureAwait(false);
    }
  }
}
