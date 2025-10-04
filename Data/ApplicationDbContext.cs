using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parcial.Models;

namespace Parcial.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Curso>(b =>
            {
                builder.Entity<Curso>(b =>
{
    b.ToTable(tb => tb.HasCheckConstraint("CK_Curso_Creditos", "Creditos > 0"));
    b.HasIndex(c => c.Codigo).IsUnique();
    b.Property(c => c.Codigo).IsRequired().HasMaxLength(50);
});

            });

            builder.Entity<Matricula>(b =>
            {
                b.HasIndex(m => new { m.UsuarioId, m.CursoId }).IsUnique();
                b.HasOne(m => m.Curso)
                    .WithMany(c => c.Matriculas)
                    .HasForeignKey(m => m.CursoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
