using System.Collections.ObjectModel;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class ItemsGridUCViewModel:ViewModelBase
{
    private ObservableCollection<ItemDataModel> _items;

    public ObservableCollection<ItemDataModel> Items
    {
        get { return _items; }
        set  =>this.RaiseAndSetIfChanged(ref _items, value);
    }

    public ItemsGridUCViewModel()
    {
        this._items = new  ObservableCollection<ItemDataModel>();
        foreach (var item in DatabaseInterface.Items)
        {
            _items.Add(new ItemDataModel(item));
        }
    }
}