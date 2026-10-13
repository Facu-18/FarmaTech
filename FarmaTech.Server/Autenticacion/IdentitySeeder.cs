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

            foreach (string role in new[] { Roles.Encargada, Roles.Empleada })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole<int>(role));
                    EnsureSucceeded(result, $"crear el rol {role}");
                }
            }

            await MigrarRolLegadoAsync(scope.ServiceProvider, roleManager);

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

            if (!await userManager.IsInRoleAsync(admin, Roles.Encargada))
            {
                IdentityResult roleResult = await userManager.AddToRoleAsync(admin, Roles.Encargada);
                EnsureSucceeded(roleResult, "asignar el rol Encargada");
            }
        }

        // The role "Administrador" was renamed to "Encargada" to match the final ERD.
        // Moves its users to the new role and removes the legacy one; idempotent.
        private static async Task MigrarRolLegadoAsync(
            IServiceProvider services,
            RoleManager<IdentityRole<int>> roleManager)
        {
            const string rolLegado = "Administrador";

            IdentityRole<int>? legado = await roleManager.FindByNameAsync(rolLegado);
            if (legado is null)
            {
                return;
            }

            var userManager = services.GetRequiredService<UserManager<Empleada>>();
            foreach (Empleada empleada in await userManager.GetUsersInRoleAsync(rolLegado))
            {
                if (!await userManager.IsInRoleAsync(empleada, Roles.Encargada))
                {
                    IdentityResult agregar = await userManager.AddToRoleAsync(empleada, Roles.Encargada);
                    EnsureSucceeded(agregar, $"asignar el rol {Roles.Encargada} a {empleada.UserName}");
                }
            }

            IdentityResult borrar = await roleManager.DeleteAsync(legado);
            EnsureSucceeded(borrar, $"eliminar el rol {rolLegado}");
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
