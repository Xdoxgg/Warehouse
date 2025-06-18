using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;

[Table("tbl_users")]
public class UserClass
{
    public UserClass()
    {
    }

    public UserClass(int id, string name, string password, bool type)
    {
        Id = id;
        Name = name;
        Password = password;
        Type = type;
    }

    [Key] [Column("PK_user_id")] public int Id { get; set; }

    [Column("user_name")] public string Name { get; set; }

    [Column("user_password")] public string Password { get; set; }
    
    [Column("type")] public bool Type { get; set; }// false - user; true - admin
}