using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;
[Table("tbl_record")]
public class Record
{

    private int _id;
    [Display(Name = "ID")]

    [Column("PK_record_id")]
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    private DateOnly _dateEntrance;
    [Display(Name = "Прибыл")]

    [Column("date_entrance")]
    public DateOnly DateEntrance
    {
        get => _dateEntrance;
        set => _dateEntrance = value;
    }

    public override string ToString()
    {
        return DateEntrance.ToString();
    }
}