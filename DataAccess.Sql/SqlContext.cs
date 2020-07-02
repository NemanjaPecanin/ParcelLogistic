using Microsoft.EntityFrameworkCore;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using System;

namespace ParcelLogistics.SKS.Package.DataAccess.Sql
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hop>().HasDiscriminator(h => h.HopType);

            modelBuilder.Entity<Truck>().HasBaseType<Hop>();
            modelBuilder.Entity<Warehouse>().HasBaseType<Hop>();
            modelBuilder.Entity<Transferwarehouse>().HasBaseType<Hop>();

            modelBuilder.Entity<Warehouse>().HasMany(wh => wh.NextHops);
        }

        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Receipient> Receipient { get; set; }
        public DbSet<HopArrival> HopArrivals { get; set; }
        public DbSet<Hop> Hops { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Transferwarehouse> Transferwarehouses { get; set; }
        public DbSet<WarehouseNextHops> NextHops { get; set; }

    }
}
