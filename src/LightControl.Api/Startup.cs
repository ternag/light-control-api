using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LightControl.Api.Infrastructure;

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
      services.AddControllers();
      services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
      services.AddSingleton<ILedContext, LedContext>();
      
      // Registre Hardware Abstraction Layer dependent on environment
      if (_env.IsDevelopment())
      {
        services.AddSingleton<IHal>((container) =>
        {
          var logger = container.GetRequiredService<ILogger<NoHardwareHAL>>();
          return new NoHardwareHAL(logger);
        });
      }
      else
      {
        services.AddSingleton<IHal>((container) =>
        {
          var logger = container.GetRequiredService<ILogger<RaspberryPiHAL>>();
          return new RaspberryPiHAL(logger);
        });
      }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      if (_env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //app.UseHttpsRedirection();

      app.UseRouting();

      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
          ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      });

      //app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
