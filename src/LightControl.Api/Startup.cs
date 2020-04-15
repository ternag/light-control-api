using LightControl.Api.AppModel;
using LightControl.Api.Hardware;
using LightControl.Api.Hardware.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using Serilog;
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
      services.AddSingleton<IHardwareFileParser, HardwareFileParser>();
      services.AddSingleton<IHardwareInfoMapper, HardwareInfoMapper>();
      services.AddSingleton<IHardwareDeviceFactory, HardwareDeviceFactory>();
      services.AddSingleton<IHardwareConfigurationFactory, HardwareConfigurationFactory>();
      
      if(_env.IsDevelopment())
      {
        services.AddCors(options => { options.AddPolicy("localDevPolicy", builder => builder.AllowAnyOrigin().Build()); });
      }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      if (_env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseCors("localDevPolicy");
      }
      else
      {
        app.UseExceptionHandler("/error");
        //app.UseSerilogRequestLogging();
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