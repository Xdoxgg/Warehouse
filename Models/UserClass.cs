using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;

[Table("tbl_users")]
public class UserClass
{
    public UserClass() 
    {
    }

    public UserClass(int id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
    }

    [Key] [Column("PK_user_id")] public int Id { get; set; }

    [Column("user_name")] public string Name { get; set; }

    [Column("user_password")] public string Password { get; set; }
}



