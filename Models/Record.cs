using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;
[Table("tbl_record")]
public class Record:IDataModel
{

    private int _id;

    [Column("PK_record_id")]
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    private DateTime _dateEntrance;
    [Column("date_entrance")]
    public DateTime DateEntrance
    {
        get => _dateEntrance;
        set => _dateEntrance = value;
    }
}