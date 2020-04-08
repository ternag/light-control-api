using LightControl.Api.Hardware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LightControl.Api.Infrastructure;
using LightControl.Api.Infrastructure.Hardware;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace LightControl.Api
{
  public class Startup
  {
    private IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      _env = env;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<HardwareOptions>(Configuration.GetSection("HardwareOptions"));
      
      services.AddControllers();
      services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));
      services.AddSingleton<ILedContext, LedContext>();
      services.AddSingleton<IHardwareContext, HardwareContext>();
      services.AddSingleton<IHardwareConfigurationLoader, HardwareConfigurationLoader>();

      // Registre Hardware Abstraction Layer dependent on environment
      if (_env.IsDevelopment())
      {
        services.AddSingleton<IHardwareConfigurationFactory, NoHardwareConfigurationFactory>();
      }
      else
      {
        services.AddSingleton<IHardwareConfigurationFactory, HardwareConfigurationFactory>();
      }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      if (_env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/error");
        app.UseSerilogRequestLogging();
      }

      app.UseRouting();

      //app.UseHttpsRedirection();

      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      });

      //app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}