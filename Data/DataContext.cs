namespace MyStore.Data;

using Microsoft.EntityFrameworkCore;
using MyStore.Models;

public class DataContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<User> Users { get; set; }
    //public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Item>()
            .Property(i => i.Cost)
            .HasPrecision(18,4);

        builder.Entity<CartItem>()
            .Property(c => c.Cost)
            .HasPrecision(18,4);

        builder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }
}