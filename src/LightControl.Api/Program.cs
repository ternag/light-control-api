using LightControl.Api.AppModel;
using LightControl.Api.Hardware;
using LightControl.Api.Hardware.Configuration;

namespace LightControl.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddSingleton(typeof(ILogger), typeof(Logger<Program>));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<ILedContext, LedContext>();
        builder.Services.AddSingleton<IHardwareContext, HardwareContext>();
        builder.Services.AddSingleton<IHardwareFileParser, HardwareFileParser>();
        builder.Services.AddSingleton<IHardwareInfoMapper, HardwareInfoMapper>();
        builder.Services.AddSingleton<IHardwareDeviceFactory, HardwareDeviceFactory>();
        builder.Services.AddSingleton<IHardwareConfigurationFactory, HardwareConfigurationFactory>();
        builder.Services.Configure<HardwareOptions>(builder.Configuration.GetSection(HardwareOptions.SectionName));
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
