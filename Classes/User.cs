using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Classes
{
    [Table("tbl_users")]
    public class User(int id, string name, string password)
    {
        public User() : this(-1, "", "") { }
        [Key]
        [Column("PK_user_id")]
        public int Id { get; set; } = id;

        [Column("user_name")]
        public string Name { get; set; } = name;

        [Column("user_password")]
        public string Password { get; set; } = password;
    }

    public class SampleContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("CONNECT_STR");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

    public static class UserRepository
    {
        private static SampleContext _context = new SampleContext(new DbContextOptions<SampleContext>());

        public static List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}