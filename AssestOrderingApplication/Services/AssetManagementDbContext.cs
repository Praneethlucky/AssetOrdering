using AssestOrderingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AssestOrderingApplication.Services
{
    public class AssetManagementDbContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderStatusType> OrderStatusTypes { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public AssetManagementDbContext(DbContextOptions<AssetManagementDbContext> options)
            : base(options)
        {
        }

        // Optional: If you have custom configurations, you can still override OnModelCreating here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatus>()
                .HasKey(os => new { os.OrderId, os.StatusTypeId }); // Composite primary key

            modelBuilder.Entity<OrderStatus>()
                .HasOne(os => os.Order)
                .WithMany(o => o.OrderStatuses)
                .HasForeignKey(os => os.OrderId);

            modelBuilder.Entity<OrderStatus>()
                .HasOne(os => os.StatusType)
                .WithMany(ost => ost.OrderStatuses)
                .HasForeignKey(os => os.StatusTypeId);

            // Optional: Additional model configurations
        }

    }
}
