using AusguckBackend;
using AusguckBackend.Services;
using DotNetEnv;
using Serilog;
using Serilog.Sinks.SystemConsole;

Env.Load("\"C:\\Git\\CampKraken\\Moduls\\AusguckBackend\\backend.env\"");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        "/app/logs/api.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30
    )
    .CreateLogger();
Globals.log = Log.Logger;

builder.Host.UseSerilog();

var app = builder.Build();
Globals.log.Information("WebApplication built");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Globals.log.Information("AusguckBackend start");
app.Run();
