using HaulageSystem.Application;
using HaulageSystem.Application.Behaviours;
using HaulageSystem.Application.Configuration.ApiOptions;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Infrastructure.DependencyInjection;
using DependencyInjection = HaulageSystem.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddSwaggerDocument(document => document.DocumentName = "swagger");

builder.AddKeyVault();

builder.ConfigureLogging(builder.Configuration, builder.Environment);
builder.Services.AddPersistence(builder.Configuration, builder.Environment);
builder.Services.AddServices();
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddConfigOptions(builder.Configuration);
builder.Services.AddMediatR(cfg =>
{
    cfg.AddOpenBehavior(typeof(TimeTrackBehaviour<,>));
    cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
});

builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

var origins = builder.Configuration.GetSection("CORS")?.Get<CorsConfigOptions>();
if (origins != null)
    app.UseCors(x => x
        .WithOrigins(origins.AllowedOriginsList())
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader());

app.SetupMiddleware();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();