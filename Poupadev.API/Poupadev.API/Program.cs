using Microsoft.EntityFrameworkCore;
using Poupadev.API.Jobs;
using Poupadev.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PoupaDevCs");
builder.Services.AddDbContext<PoupaDevContext>(item => item.UseInMemoryDatabase("PoupaDevDb"));

builder.Services.AddHostedService<RendimentoAutomaticoJob>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
