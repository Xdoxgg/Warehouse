using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;


namespace Warehouse.ViewModels;

public class ControlWindowViewModel : ViewModelBase
{
    #region Properties

    private int _selectedIndex;

    public int SelectedIndex
    {
        get => _selectedIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
    }

    private string _searchText;

    public string SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    private string _searchLabelText;

    public string SearchLabelText
    {
        get => _searchLabelText;
        set => this.RaiseAndSetIfChanged(ref _searchLabelText, value);
    }

    #endregion

    #region DataCollections

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

    #endregion

    #region Commands

    private ReactiveCommand<Unit, Unit> _loadDataCommand;

    public ReactiveCommand<Unit, Unit> LoadDataCommand
    {
        get => _loadDataCommand;
        set => _loadDataCommand = value;
    }

    private ReactiveCommand<Unit, Unit> _searchCommand;

    public ReactiveCommand<Unit, Unit> SearchCommand
    {
        get => _searchCommand;
        set => _searchCommand = value;
    }
    
    #endregion

    #region Functions

    private async Task<Unit> LoadData()
    {
        switch (SelectedIndex)
        {
            case 0:
            {
                SearchLabelText = "Введите название";
                break;
            }
            case 1:
            {
                SearchLabelText = "Введите дату прибытия";
                break;
            }
            case 2:
            {
                SearchLabelText = "Введите название";
                break;
            }
        }

        return Unit.Default;
    }

    private async Task<Unit> Search()
    {
        Console.WriteLine(SearchText);
        return Unit.Default;
    }
    
    #endregion

    public ControlWindowViewModel()
    {
        SelectedIndex = 0;
        SearchText = "";
        SearchLabelText = "";
        Items = new ObservableCollection<Item>(DatabaseInterface.Items);
        Records = new ObservableCollection<Record>(DatabaseInterface.Records);
        ItemTypes = new ObservableCollection<ItemType>(DatabaseInterface.ItemTypes);
        LoadDataCommand = ReactiveCommand.CreateFromTask(LoadData);
        SearchCommand = ReactiveCommand.CreateFromTask(Search);
    }
}