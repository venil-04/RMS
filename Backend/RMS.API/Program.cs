using Microsoft.EntityFrameworkCore;
using RMS.API.Middleware;
using RMS.Application.Interfaces;
using RMS.Application.Services;
using RMS.Infrastructure.Persistence;
using RMS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Application Services
builder.Services.AddScoped<IMenuCategoryService, MenuCategoryService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
