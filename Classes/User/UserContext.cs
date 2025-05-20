using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Classes.User;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        var connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }
}