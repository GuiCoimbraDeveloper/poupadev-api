using Microsoft.EntityFrameworkCore;
using Poupadev.API.Entities;

namespace Poupadev.API.Persistence
{
    public class PoupaDevContext : DbContext
    {
        public PoupaDevContext(DbContextOptions<PoupaDevContext> options) : base(options)
        {
        }

        public DbSet<ObjetivoFinanceiro> Objetivos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ObjetivoFinanceiro>(o =>
            {
                o.HasKey(of => of.Id);

                o.Property(of => of.ValorObjetivo).HasColumnType("decimal(18,4)");
                o.HasMany(of => of.Operacoes).WithOne().HasForeignKey(of => of.IdObjetivo)
                .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Operacao>(e =>
            {
                e.HasKey(op => op.Id);
                e.Property(op => op.Valor).HasColumnType("decimal(18,4)");

            });
            base.OnModelCreating(builder);
        }
    }
}
