using Microsoft.EntityFrameworkCore;

namespace App.Models
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<AuditTrail> AuditTrails { get;  set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(12,2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("userId");
                entity.Property(e => e.UserName).HasColumnName("user_name").HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.UserRole).HasColumnName("user_role").HasMaxLength(20);
                entity.Property(e => e.Password).HasColumnName("password").HasMaxLength(100);
            });


            modelBuilder.Entity<AuditTrail>(entity =>
            {
                entity.ToTable("AuditTrail");
                entity.HasKey(e => e.id);
                entity.Property(e => e.userid).HasColumnName("user_id");
                entity.Property(e => e.operationtype).HasColumnName("operation_type");
                entity.Property(e => e.datetime).HasColumnName("date_time");
                entity.Property(e => e.description).HasColumnName("description");
                entity.Property(e => e.primarykey).HasColumnName("primary_key");

            });
        }
    }
}