using ASPEKT.Application;
using ASPEKT.Application.Settings;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);

AppSettings appSettingsObject = appSettings.Get<AppSettings>();

DependencyInjection.InjectDbContext(builder.Services, appSettingsObject.DbConnectionString);
DependencyInjection.InjectRepositories(builder.Services);
DependencyInjection.InjectServices(builder.Services);

Log.Logger = new LoggerConfiguration()
           .Enrich.FromLogContext()
           .MinimumLevel.Information()
           .WriteTo.File(
               $@"{AppDomain.CurrentDomain.BaseDirectory}Loggers\ASPEKT.Application.LOG{DateTime.Now.Date:dd-MM-yyyy}.txt",
               LogEventLevel.Information,
               "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}",
               rollingInterval: RollingInterval.Day) //Every day new file for loggers
           .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
