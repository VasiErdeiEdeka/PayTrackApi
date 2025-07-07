using Microsoft.EntityFrameworkCore;
using PayTrack.API.Middleware;
using PayTrack.Application.Interfaces;
using PayTrack.Domain.Entities;
using PayTrack.Infrastructure.Context;
using PayTrack.Infrastructure.Mappings;
using PayTrack.Infrastructure.Repositories;
using PayTrack.Infrastructure.Services;
using Serilog;
using System.Reflection;
using PayTrack.API;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

try
{
    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddDbContext<PayTrackDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddScoped<IRepository<User>, EfRepository<User>>();
    builder.Services.AddScoped<IRepository<Transaction>, EfRepository<Transaction>>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ITransactionService, TransactionService>();

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ExecuteMigrations();

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Error(exception, "Host terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
