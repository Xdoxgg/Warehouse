using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class EditItemViewModel : ViewModelBase
{
   

    public ObservableCollection<Record> Records { get; }
    
    public ObservableCollection<ItemType> ItemTypes { get; }
    private Item _item;

    public Item Item
    {
        get => _item;
        set => this.RaiseAndSetIfChanged(ref _item, value);
    }

    public EditItemViewModel(Item item)
    {
        
        ItemTypes = new ObservableCollection<ItemType>( DatabaseInterface.ItemTypes);
        Records = new ObservableCollection<Record>( DatabaseInterface.Records);
        Item = item;
    
    }
}