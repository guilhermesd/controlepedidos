using Application.Middleares;
using Application.UseCases.Clientes;
using Application.UseCases.Produtos;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependências para Repositórios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Injeção de dependências para Use Cases
builder.Services.AddScoped<ISalvarClienteUseCase, SalvarClienteUseCase>();
builder.Services.AddScoped<IObterClientePorCpfUseCase, ObterClientePorCpfUseCase>();
builder.Services.AddScoped<IRemoverClienteUseCase, RemoverClienteUseCase>();

builder.Services.AddScoped<ISalvarProdutoUseCase, SalvarProdutoUseCase>();
builder.Services.AddScoped<IRemoverProdutoUseCase, RemoverProdutoUseCase>();
builder.Services.AddScoped<IObterProdutosUseCase, ObterProdutosUseCase>();


// Adiciona o DbContext com MySQL com resiliência a erros transitórios
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                     new MySqlServerVersion(new Version(8, 0, 32)),
                     mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionsMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage] // Exclui toda a classe do coverage
public partial class Program { }
