using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Models;
using Microsoft.EntityFrameworkCore;

namespace HardReserve.Contexts
{
    public class HardReserveDbContext : DbContext
    {
    
        public HardReserveDbContext(DbContextOptions<HardReserveDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Kit> Kit { get; set; }
        public DbSet<Hardware> Hardware { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Hardware_Reserva> Hardware_Reserva { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hardware_Reserva>()
                .HasKey(hr => new { hr.Reserva_Id, hr.Hardware_Id });
        }
    }
    }