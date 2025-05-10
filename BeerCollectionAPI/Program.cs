using BeerCollection.Application.Beers.Commands;
using BeerCollection.Domain.Interfaces;
using BeerCollection.Infrastructure.Persistence;
using BeerCollection.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add appsettings.json support
var configuration = builder.Configuration;

// Register DbContext
builder.Services.AddDbContext<BeerCollectionDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateBeerCommand).Assembly);
});

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateBeerCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(CreateBeerCommand).Assembly);

// Register repositories
builder.Services.AddScoped<IBeerRepository, BeerRepository>();

// Add controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();