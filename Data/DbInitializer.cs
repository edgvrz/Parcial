using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Parcial.Models;

namespace Parcial.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();

            if (!await roleManager.RoleExistsAsync("Coordinador"))
            {
                await roleManager.CreateAsync(new IdentityRole("Coordinador"));
            }

            var coordEmail = "coordinador@universidad.local";
            var coord = await userManager.FindByEmailAsync(coordEmail);
            if (coord == null)
            {
                coord = new IdentityUser { UserName = coordEmail, Email = coordEmail, EmailConfirmed = true };
                var password = Environment.GetEnvironmentVariable("COORDINADOR_PASSWORD") ?? "Coordinator123!";
                var create = await userManager.CreateAsync(coord, password);
                if (create.Succeeded)
                {
                    await userManager.AddToRoleAsync(coord, "Coordinador");
                }
            }

            if (!context.Cursos.Any())
            {
                context.Cursos.AddRange(
                    new Curso { Codigo = "MAT101", Nombre = "Matemáticas I", Creditos = 3, CupoMaximo = 30, HorarioInicio = new TimeSpan(8,0,0), HorarioFin = new TimeSpan(10,0,0), Activo = true },
                    new Curso { Codigo = "PROG100", Nombre = "Introducción a la Programación", Creditos = 4, CupoMaximo = 25, HorarioInicio = new TimeSpan(10,30,0), HorarioFin = new TimeSpan(12,30,0), Activo = true },
                    new Curso { Codigo = "FIS101", Nombre = "Física I", Creditos = 3, CupoMaximo = 20, HorarioInicio = new TimeSpan(14,0,0), HorarioFin = new TimeSpan(16,0,0), Activo = true }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
