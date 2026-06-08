using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Models;
using Microsoft.EntityFrameworkCore;

namespace HardReserve.Contexts
{
    public class HardReserveDbContext
    {
        public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Kit> Kits { get; set; }
        public DbSet<Hardware> Hardwares { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Hardware_Reserva> Hardware_Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hardware_Reserva>()
                .HasKey(hr => new { hr.Reserva_Id, hr.Hardware_Id });
        }
    }
    }
}