using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LightControl.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables("LC_")
        .Build();
      
      Log.Logger = new LoggerConfiguration()
         .ReadFrom.Configuration(configuration)
         .CreateLogger();
      
      try
      {
        Log.Information($"Starting up '{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}' environment");
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Application start-up failed");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }
}