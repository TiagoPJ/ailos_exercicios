using AutoMapper;
using FluentAssertions.Common;
using IdempotentAPI.Cache.DistributedCache.Extensions.DependencyInjection;
using MediatR;
using Microsoft.OpenApi.Models;
using Questao5.Infrastructure.Database.Extensao;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Infrastructure.Sqlite;
using Questao5.Utils;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
// mapper
builder.Services.AddSingleton(AdapterMapper.MapperRegister());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    c =>
    {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Swagger Documentação Web API",
            Description = "Documentação API V1.",
            Contact = new OpenApiContact() { Name = "Tiago Jesus", Email = "amcom-tiago.jesus@ailos.coop.br" },
            License = new OpenApiLicense() { Name = "MIT License", Url = new Uri("https://opensource.org/licenses/MIT") }
        });
        c.OperationFilter<SwaggerConfiguration>(); // Adiciona obrigatoriedade do IdempontencyKey
    }    
);

// Idempontent
builder.Services.AddDistributedMemoryCache();
builder.Services.AddIdempotentAPIUsingDistributedCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
app.Services.GetService<IAccountRepository>().Iniciar();
#pragma warning restore CS8602 // Dereference of a possibly null reference.



app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


