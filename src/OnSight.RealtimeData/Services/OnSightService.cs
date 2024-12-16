using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnSight.RealtimeData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnSight.RealtimeData.Services
{
  public interface IOnSightCoreService
  {
    Task EnsureAllConnected(IServiceProvider serviceProvider, ILogger<IOnSightCoreService> logger);
  }

  public class OnSightService : IOnSightCoreService
  {
    public Dictionary<Guid, HubConnection> connections { get; set; } = new Dictionary<Guid, HubConnection>();

    public async Task EnsureAllConnected(IServiceProvider serviceProvider, ILogger<IOnSightCoreService> logger)
    {
      await ConnectToOnSight(new CompanyCredentialDto
      {
        // Will be provided
        OnSightId = Guid.NewGuid(),
        // Will be Provided
        Token = "",
        Url = "https://api.on-sight.co.nz"
      }, serviceProvider);

      await Task.CompletedTask;
    }

    private async Task ConnectToOnSight(CompanyCredentialDto credentials, IServiceProvider serviceProvider)
    {
      var url = credentials.Url;
      var token = credentials.Token;
      var companyId = credentials.OnSightId;

      if (connections.ContainsKey(companyId))
      {
        return;
      }

      var connection = new HubConnectionBuilder()
        .ConfigureLogging(lo =>
        {
          lo.AddConsole();
        })
        .WithUrl($"{url.TrimEnd('/')}/sensordata", co =>
        {
          co.Headers.Add("Authorization", $"Bearer {credentials.Token}");
        })
        .Build();

      await connection.StartAsync();

      await connection.InvokeAsync("RequestCompanyDevicesFeed", credentials.OnSightId);
      await connection.InvokeAsync("RequestCompanyDeviceZoneChange", credentials.OnSightId);

      connection.Closed += (ex) =>
      {
        connections.Remove(companyId);

        return Task.CompletedTask;
      };

      connection.On<DeviceChangeBatch>(
        "CompanyDeviceSensorsUpdate", async updateDto =>
        {
          // process update
        });

      connection.On<Breach>(
        "CompanySensorBreach", async updateDto =>
        {
          // Sensor breach
        });

      connection.On<CoreDeviceZoneChange>(
        "CompanyDeviceZoneChange", async updateDto =>
        {
          // Sensor entered or left a zone
        });

      connections[companyId] = connection;
    }
  }
}
