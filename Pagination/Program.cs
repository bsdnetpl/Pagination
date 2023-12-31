using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Pagination.DB;
using Pagination.Middleware;
using Pagination.Models;
using Pagination.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserServices, UserServices>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ConnectMssql>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS")));
builder.Services.AddScoped<IValidator<UserQuery>, UserQueryValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<UserQueryValidation>();
builder.Services.AddScoped<Validation>(); // remember  add this in controller: using FluentValidation.Results;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<Validation>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
