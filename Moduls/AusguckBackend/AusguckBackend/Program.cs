using AusguckBackend;
using AusguckBackend.Services;
using DotNetEnv;

Env.Load("\"C:\\Git\\CampKraken\\Moduls\\AusguckBackend\\backend.env\"");
Globals.log.Information("Environment variables loaded");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
Globals.log.Information("AusguckBackend started");