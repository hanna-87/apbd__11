using APBD_11.DAL;
using APBD_11.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>( opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();

