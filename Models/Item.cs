using System;
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
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    
    [Column("name")]
    public string Name
    {
        get => _name;
        set => _name = value;
    }
    
    [Column("cost")]
    public double Price
    {
        get => _price;
        set => _price = value;
    }

    [Column("description")]
    public string Description
    {
        get => _description;
        set => _description = value;
    }

    [Column("to_date")]
    public DateOnly? ToDate
    {
        get => _toDate;
        set => _toDate = value;
    }

    [ForeignKey("ItemType")]
    [Column("FK_type_id")]
    public int? ItemTypeId
    {
        get => _itemType_id;
        set => _itemType_id = value;
    }

    public ItemType ItemType
    {
        get => _item_type;
        set => _item_type = value;
    }

    [ForeignKey("Record")]
    [Column("FK_record_id")]
    public int? RecordId
    {
        get => _recordId;
        set => _recordId = value;
    }

    public Record? Record
    {
        get => _record;
        set => _record = value;
    }
}