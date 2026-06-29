using AutomatedTesting.Application;
using AutomatedTesting.Application.Common.Behaviors;
using AutomatedTesting.Application.Features.Tasks.Commands.CreateTask;
using AutomatedTesting.Db.AppDbContextModels;
using AutomatedTesting.Repositories.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});


builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();       
    app.UseSwaggerUI();     
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
