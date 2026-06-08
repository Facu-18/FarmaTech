using FarmaTech.BD.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("ConnSqlServer")
    ?? throw new InvalidOperationException("No existe la conexión con la base de datos.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.Run();
