using Domain.Interfaces.IDBPROXIES;
using Domain.Interfaces.IServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data.DBProxies;
using Infrastrucutre.Buisnes.Services;
using RegistrationApi;
using RegistrationApi.Examples;
using Serilog;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<RegisterInAppExample>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() 
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);


string dbProxy = String.Empty;
if (builder.Environment.IsDevelopment())
{
    // для обычного запуска
    var configuration = builder.Configuration;
    dbProxy = configuration["ApiSettings:DbProxy"] ?? throw new Exception("DbProxy is missing");
}
else
{
    //докерок
    dbProxy = Environment.GetEnvironmentVariable("DbProxy") ?? "http://localhost"; 
    if (!Uri.IsWellFormedUriString(dbProxy, UriKind.Absolute))
    {
        dbProxy = "http://localhost";
    }  
}





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

