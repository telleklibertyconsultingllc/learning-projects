using EX.EFC.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EX.EFC.Models
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SamuraiAppData");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasKey(s => new { s.Id, s.Name });
            /// Shadow Properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
            }
            /// Owned Types
            modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName).Property(b => b.GivenName).HasColumnName("GivenName");
            modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName).Property(b => b.SurName).HasColumnName("SurName");
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timeStamp = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries()
                                  .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                /// Do not check Own Types
                if (entry.Metadata.IsOwned())
                {
                    continue;
                }
                /// Shadow Properties
                entry.Property("LastModified").CurrentValue = timeStamp;
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = timeStamp;
                }
            }
            return base.SaveChanges();
        }
    }
}
