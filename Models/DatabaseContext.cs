using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Models;

internal class DatabaseContext:DbContext
{
    public DbSet<UserClass> Users { get; set; }
    public DbSet<ItemType> ItemTypes { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        var connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }
}