using System.Text;
using System.Security.Claims;
using FarmaTech.BD.Datos;
using FarmaTech.BD.Datos.Entity;
using FarmaTech.Repository.Repositorios;
using FarmaTech.Server.Autenticacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("ConnSqlServer")
    ?? throw new InvalidOperationException("No existe la conexión con la base de datos.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

JwtOptions jwtOptions = builder.Configuration
    .GetRequiredSection(JwtOptions.SectionName)
    .Get<JwtOptions>()
    ?? throw new InvalidOperationException("No existe la configuracion JWT.");

if (Encoding.UTF8.GetByteCount(jwtOptions.Key) < 32)
{
    throw new InvalidOperationException("Jwt:Key debe tener al menos 32 caracteres.");
}

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetRequiredSection(JwtOptions.SectionName));

builder.Services
    .AddIdentityCore<Empleada>(options =>
    {
        options.Password.RequiredLength = 4;
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Ingrese el token JWT sin el prefijo Bearer."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});
builder.Services.AddRazorPages();
builder.Services.AddScoped<ITokenService, TokenService>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

await IdentitySeeder.SeedAsync(app.Services, app.Configuration);

app.Run();
