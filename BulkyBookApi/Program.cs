using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System.Net;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Bulky.DataAccess")));
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    var app = builder.Build();

    app.UseExceptionHandler(builder =>
    {
        builder.Run(async context =>
        {
            var logger = builder.ApplicationServices.GetRequiredService<ILogger<Program>>();
            var feature = context.Features.Get<IExceptionHandlerFeature>();

            if (feature.Error is not null)
            {
                logger.LogError(feature.Error, "Exception error logged");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    error = "Something went wrong",
                    details = feature.Error.Message
                }));
            }
        });
    });

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
catch (Exception ex)
{
    logger.Error(ex,"An error occurred");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
