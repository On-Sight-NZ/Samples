using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnSight.RealtimeData.Services;
using OnSight.RealtimeData.Services.Jobs;
using Quartz.Impl;

namespace OnSight.RealtimeData
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IOnSightCoreService, OnSightService>();

      // Quartz Config
      var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
      services.AddSingleton(scheduler);
      services.AddHostedService<JobHostedService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
    }
  }
}
