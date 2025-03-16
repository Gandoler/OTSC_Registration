using Domain.Interfaces.IDBPROXIES;
using Domain.Interfaces.IServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data.DBProxies;
using Infrastrucutre.Buisnes.Services;
using RegistrationApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() 
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

var configuration = builder.Configuration;
var dbProxy = configuration["ApiSettings:DbProxy"] ?? throw new Exception("DbProxy is missing");
builder.Services.AddHttpClient("ProxyApiClient", client =>
{
    client.BaseAddress = new Uri(dbProxy);
});


builder.Services.AddScoped<IRegistrProxy>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ProxyApiClient");
    return new RegistrProxy(httpClient,Log.Logger);
});
builder.Services.AddScoped<ICheckExistProxy>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ProxyApiClient");
    return new CheckExistProxy(httpClient,Log.Logger);
});

builder.Services.AddScoped<IRegistrService, RegistrService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<DtoValidatorMiddleware>();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseSerilogRequestLogging();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
   
}

app.UseHttpsRedirection();



app.Run();

