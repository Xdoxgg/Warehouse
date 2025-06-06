using System;

namespace Warehouse.Models;

public class ItemDataModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public DateOnly? ToDate { get; set; }
    public string Category { get; set; }
    public DateOnly? RecordDate { get; set; }

    public ItemDataModel(Item item)
    {
        Id = item.Id;
        Name = item.Name;
        Price = item.Price;
        Description = item.Description;
        if (item.ToDate != null)
            ToDate = new DateOnly(item.ToDate.Value.Year, item.ToDate.Value.Month, item.ToDate.Value.Day);
        else
        {
            ToDate =new  DateOnly(1,1,1);
        }
        Category = item.ItemType.Name;
        if (item.Record != null)
            RecordDate = new DateOnly(item.Record.DateEntrance.Year, item.Record.DateEntrance.Month,
                item.Record.DateEntrance.Day);
        else
        {
            ToDate =new  DateOnly(1,1,1);

        }
    }
}