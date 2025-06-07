using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class ItemsGridUCViewModel:ViewModelBase
{
    private ObservableCollection<Item> _items;

    public ObservableCollection<Item> Items
    {
        get { return _items; }
        set  =>this.RaiseAndSetIfChanged(ref _items, value);
    }


    public ItemsGridUCViewModel()
    {
        _items = new  ObservableCollection<Item>(DatabaseInterface.Items);
        Console.WriteLine(_items.Count);
    }
}