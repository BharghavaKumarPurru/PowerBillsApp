using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CurrentBillApp.Models;
namespace CurrentBillApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Bill> Bills { get; set; }
    public DbSet<TransformedBill> TransformedBills { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.BillNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasMaxLength(200);

                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}


