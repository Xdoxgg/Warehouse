using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using Warehouse.Models;


namespace Warehouse.ViewModels;

public class ControlWindowViewModel : ViewModelBase
{
    private ObservableCollection<Item> _items;

    public ObservableCollection<Item> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }
    private ObservableCollection<Record> _records;

    public ObservableCollection<Record> Records
    {
        get => _records;
        set => this.RaiseAndSetIfChanged(ref _records, value);
    }
    private ObservableCollection<ItemType> _itemTypes;

    public ObservableCollection<ItemType> ItemTypes
    {
        get => _itemTypes;
        set => this.RaiseAndSetIfChanged(ref _itemTypes, value);
    }
    public ControlWindowViewModel()
    {
        Items = new ObservableCollection<Item>(DatabaseInterface.Items);
        Records = new ObservableCollection<Record>(DatabaseInterface.Records);
        ItemTypes= new ObservableCollection<ItemType>(DatabaseInterface.ItemTypes);
    }
}