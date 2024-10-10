using Numer.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Numer.Core;
using System.Net.NetworkInformation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Adding MediatR
builder.Services.AddCoreService();


builder.Services.AddInfrastructureServices(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*
app.UseExceptionHandler(config => {
    config.Run(async context => {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text";

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null) {
            await context.Response.WriteAsync("Something wrong.");
        }
    });
});
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
