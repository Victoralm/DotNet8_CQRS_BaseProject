using System.Reflection;
using API.Context;
using API.UnitOfWork.Implementations;
using API.UnitOfWork.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add logging services
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

//DbContext
builder.Services.AddDbContext<PostgreContext>();
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<PostgreContext>();
dataContext.Database.EnsureCreated();

//UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registers MediatR and scans all handlers of the current assembly.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

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

app.Run();
