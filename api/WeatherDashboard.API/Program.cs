using Serilog;
using WeatherDashboard.API.Interfaces;
using WeatherDashboard.API.Model.OpenWeatherMap;
using WeatherDashboard.API.Services;
using HttpClient = WeatherDashboard.API.Services.HttpClient;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddSingleton<IForecastService, ForecastService>()
    .AddSingleton<IDefaultLocationService, DefaultLocationService>()
    .AddSingleton<IHttpClient, HttpClient>()
    .AddHttpClient()
    .AddMemoryCache()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    })
    .AddOptions<ApiSettings>()
    .Bind(builder.Configuration.GetSection("OpenWeatherMap"));
    
var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigins"); 
app.UseHttpsRedirection();
app.Run();