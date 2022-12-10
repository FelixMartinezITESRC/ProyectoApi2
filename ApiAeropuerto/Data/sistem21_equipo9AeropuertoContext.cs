using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiAeropuerto.Models;

namespace ApiAeropuerto.Data
{
    public partial class sistem21_equipo9AeropuertoContext : DbContext
    {
        public sistem21_equipo9AeropuertoContext()
        {
        }

        public sistem21_equipo9AeropuertoContext(DbContextOptions<sistem21_equipo9AeropuertoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Vuelos> Vuelos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Vuelos>(entity =>
            {
                entity.ToTable("vuelos");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CodigoVuelo).HasMaxLength(6);

                entity.Property(e => e.Destino).HasMaxLength(30);

                entity.Property(e => e.Estado)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'En camino'");

                entity.Property(e => e.HorarioSalida).HasColumnType("datetime");

                entity.Property(e => e.PuertaSalida).HasMaxLength(6);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
