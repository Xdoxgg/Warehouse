using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Classes.User;

[Table("tbl_users")]
public class User(int id, string name, string password)
{
    public User() : this(-1, "", "")
    {
    }

    [Key] [Column("PK_user_id")] public int Id { get; set; } = id;

    [Column("user_name")] public string Name { get; set; } = name;

    [Column("user_password")] public string Password { get; set; } = password;
}



