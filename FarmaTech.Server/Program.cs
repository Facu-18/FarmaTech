using FarmaTech.BD.Datos;
using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("ConnSqlServer")
    ?? throw new InvalidOperationException("No existe la conexión con la base de datos.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmpleadaRepositorio, EmpleadaRepositorio>();
builder.Services.AddScoped<IRepositorio<Producto>, Repositorio<Producto>>();
builder.Services.AddScoped<IRepositorio<Drogueria>, Repositorio<Drogueria>>();
builder.Services.AddScoped<IRepositorio<Venta>, Repositorio<Venta>>();
builder.Services.AddScoped<IRepositorio<DetalleVenta>, Repositorio<DetalleVenta>>();
builder.Services.AddScoped<IRepositorio<IngresoMercaderia>, Repositorio<IngresoMercaderia>>();
builder.Services.AddScoped<IRepositorio<NotaPedido>, Repositorio<NotaPedido>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
