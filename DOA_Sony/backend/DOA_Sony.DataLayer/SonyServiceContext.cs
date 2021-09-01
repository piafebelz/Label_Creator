using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace DOA_Sony.DataLayer
{
    public class SonyServiceContext : DbContext
    {
        public SonyServiceContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<APNS> APNS { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Control> Control { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<Change> Change { get; set; }
        public DbSet<Part> Part { get; set; }
        public DbSet<ProductTypeControl> ProductTypeControl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>(e =>
            {
                e.HasMany(x => x.ProductTypeControls).WithOne(x => x.ProductType).HasForeignKey(x => x.ProductTypeID).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Control>(e =>
            {
                e.HasMany(x => x.ProductTypeControls).WithOne(x => x.Control).HasForeignKey(x => x.ControlID).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductTypeControl>(e =>
            {
                e.HasOne(r => r.ProductType).WithMany(u => u.ProductTypeControls).HasForeignKey(r => r.ProductTypeID);
                e.HasOne(r => r.Control).WithMany(u => u.ProductTypeControls).HasForeignKey(r => r.ControlID);
            });
        }
    }
}
