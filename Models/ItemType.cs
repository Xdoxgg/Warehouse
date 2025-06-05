using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;
[Table("tbl_item_type")]
public class ItemType:IDataModel
{
    private int _id;
    private string _name;
    private string _description;
    [Column("PK_type_id")]
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    [Column("type_name")]
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    [Column("description")]
    public string Description
    {
        get => _description;
        set => _description = value;
    }
    
    public ItemType()
    {
        
    }
    
}