using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;
[Table("tbl_creator")]
public class Creator
{
    private int _id;
    private string _name;
    [Column("PK_creator_id")]
    [Display(Name = "ID")]
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    
    [Column("name")]
    [Display(Name = "Название")]
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}