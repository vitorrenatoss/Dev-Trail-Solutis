namespace BankSystem.Api;
using BankSystem.Api.Contexts;
using BankSystem.Api.Controllers;
using BankSystem.Api.Repositories;
using BankSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<BankContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BankDatabase")));

        builder.Services.AddScoped<IContaRepository, ContaRepository>();
        builder.Services.AddScoped<IContaService, ContaService>();

        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<IClienteService, IClienteService>();

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

        app.MapGet("/health", async (BankContext context) =>
        {

            try
            {
                bool conexaoOK = await context.Database.CanConnectAsync();
                if (conexaoOK)
                {
                    return Results.Ok("O banco está funcionando.");
                }
                else
                {
                    return Results.Problem("Não foi possível conectar ao banco de dados.");
                }
            }
            catch (Exception ex)
            {
                return Results.Problem($"Meu banco falhou: {ex.Message}");
            }
        });

        app.Run();
    }
    
}



