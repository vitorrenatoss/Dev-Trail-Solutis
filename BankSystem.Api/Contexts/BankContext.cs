namespace BankSystem.Api.Contexts;

using BankSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.CodeDom;

public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Conta> Contas { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Conta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Saldo).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Tipo).IsRequired();

            entity.HasOne(c => c.Cliente)
                  .WithMany(cl => cl.Contas)
                  .HasForeignKey(c => c.ClienteId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.CPF).IsRequired();
            entity.HasIndex(e => e.CPF).IsUnique();
            entity.Property(e => e.DataNascimento).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });

    }
   
    
}


