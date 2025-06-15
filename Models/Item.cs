using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;

[Table("tbl_item")]
public class Item
{
    private int _id;
    private string _name;
    private double _price;
    private string _description;
    private DateOnly? _toDate;
    private int? _itemType_id;
    private ItemType? _item_type;
    private int? _recordId;
    private Record? _record;

    [Column("PK_item_id")]
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
    
    [Column("cost")]
    [Display(Name = "Стоимость")]

    public double Price
    {
        get => _price;
        set => _price = value;
    }

    [Column("description")]
    [Display(Name = "Описание")]

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    [Column("to_date")]
    [Display(Name = "Годен до")]

    public DateOnly? ToDate
    {
        get => _toDate;
        set => _toDate = value;
    }

    [ForeignKey("ItemType")]
    [Column("FK_type_id")]
    internal int? ItemTypeId
    {
        get => _itemType_id;
        set => _itemType_id = value;
    }
    [Display(Name = "Тип")]

    public ItemType ItemType
    {
        get => _item_type;
        set => _item_type = value;
    }

    [ForeignKey("Record")]
    [Column("FK_record_id")]
    internal int? RecordId
    {
        get => _recordId;
        set => _recordId = value;
    }
    [Display(Name = "Прибыл")]

    public Record? Record
    {
        get => _record;
        set => _record = value;
    }

   
}