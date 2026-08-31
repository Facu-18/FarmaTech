using FarmaTech.BD.Datos.Entity;
using Microsoft.AspNetCore.Identity;

namespace FarmaTech.Server.Autenticacion
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            IServiceProvider services,
            IConfiguration configuration)
        {
            using IServiceScope scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            foreach (string role in new[] { Roles.Administrador, Roles.Empleada })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole<int>(role));
                    EnsureSucceeded(result, $"crear el rol {role}");
                }
            }

            IConfigurationSection adminSection = configuration.GetSection("BootstrapAdmin");
            string? usuario = adminSection["Usuario"];
            string? pin = adminSection["Pin"];

            if (string.IsNullOrWhiteSpace(usuario) && string.IsNullOrWhiteSpace(pin))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(pin) ||
                !System.Text.RegularExpressions.Regex.IsMatch(pin, @"^\d{4,6}$"))
            {
                throw new InvalidOperationException(
                    "BootstrapAdmin debe contener un Usuario y un PIN de 4 a 6 digitos.");
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Empleada>>();
            Empleada? admin = await userManager.FindByNameAsync(usuario);

            if (admin is null)
            {
                admin = new Empleada
                {
                    UserName = usuario,
                    Nombre = adminSection["Nombre"] ?? "Administrador",
                    Apellido = adminSection["Apellido"] ?? "FarmaTech"
                };

                IdentityResult createResult = await userManager.CreateAsync(admin, pin);
                EnsureSucceeded(createResult, "crear el administrador inicial");
            }

            if (!await userManager.IsInRoleAsync(admin, Roles.Administrador))
            {
                IdentityResult roleResult = await userManager.AddToRoleAsync(admin, Roles.Administrador);
                EnsureSucceeded(roleResult, "asignar el rol Administrador");
            }
        }

        private static void EnsureSucceeded(IdentityResult result, string operation)
        {
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"No se pudo {operation}: {errors}");
            }
        }
    }
}
