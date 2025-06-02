using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Models;

public class DatabaseContext:DbContext
{
    public DbSet<UserClass> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        var connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }
}