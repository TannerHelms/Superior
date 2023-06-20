using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Superior_Cloud_Accounting.Models;

public partial class SuperiorDbContext : DbContext
{
    public SuperiorDbContext()
    {
    }

    public SuperiorDbContext(DbContextOptions<SuperiorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Form> Forms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=192.185.52.192;port=3306;database=tannerhe_Superior;uid=tannerhelms;password=tH2001066580", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.23-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_unicode_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnType("int(20)");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Message).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
