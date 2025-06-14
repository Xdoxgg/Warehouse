using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class ControlWindowViewModel : ViewModelBase
{
    #region Properties

    #region Error

    private double _errorOpacity;

    public double ErrorOpacity
    {
        get => _errorOpacity;
        set => this.RaiseAndSetIfChanged(ref _errorOpacity, value);
    }

    public string ErrorText
    {
        get => _errorText;
        set => this.RaiseAndSetIfChanged(ref _errorText, value);
    }


    public async Task<Unit> HideError()
    {
        Thread.Sleep(3000);
        ErrorText = "";
        ErrorOpacity = 0;
        return Unit.Default;
    }

    private string _errorText;

    #endregion

    private bool _buttonSearchVisibility;

    public bool ButtonSearchVisibility
    {
        get => _buttonSearchVisibility;
        set => this.RaiseAndSetIfChanged(ref _buttonSearchVisibility, value);
    }

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


    private object _selectedDataGridItem;

    public object SelectedDataGridItem
    {
        get => _selectedDataGridItem;
        set => this.RaiseAndSetIfChanged(ref _selectedDataGridItem, value);
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
    
    private ReactiveCommand<Unit, Unit> _addRowCommand;

    public ReactiveCommand<Unit, Unit> AddRowCommand
    {
        get => _addRowCommand;
        set => _addRowCommand = value;
    }

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

    private ReactiveCommand<Unit, Unit> _searchByDateCommand;

    public ReactiveCommand<Unit, Unit> SearchByDateCommand
    {
        get => _searchByDateCommand;
        set => _searchByDateCommand = value;
    }

    #endregion

    #region Functions

    private async Task<Unit> LoadData()
    {
        switch (SelectedIndex)
        {
            case 0:
            {
                ButtonSearchVisibility = false;
                SearchLabelText = "Введите название";
                break;
            }
            case 1:
            {
                ButtonSearchVisibility = true;
                SearchLabelText = "Введите дату прибытия";
                break;
            }
            case 2:
            {
                ButtonSearchVisibility = false;
                SearchLabelText = "Введите название";
                break;
            }
        }

        return Unit.Default;
    }

    private async Task<Unit> Search()
    {
        switch (SelectedIndex)
        {
            case 0:
            {
                var result = Items.First(x => x.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase));
                SelectedDataGridItem = result;
                break;
            }
            // type record search at SearchByDate() method
            case 2:
            {
                var result =
                    ItemTypes.First(x => x.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase));
                SelectedDataGridItem = result;
                break;
            }
        }


        return Unit.Default;
    }

    private async Task<Unit> SearchByDate()
    {
        try
        {
            DateOnly timeSearch = DateOnly.ParseExact(SearchText, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var result = Records.First(x => x.DateEntrance == timeSearch);
            SelectedDataGridItem = result;
            Console.WriteLine(result.Id);
        }
        catch (FormatException)
        {
            ErrorText = "Неверный формат даты(дд.мм.гггг)";
            ErrorOpacity = 0.5;
            Task.Run(HideError);
        }
        catch (Exception ex)
        {
            ErrorText = ex.Message;
            ErrorOpacity = 0.5;
            Task.Run(HideError);
        }

        return Unit.Default;
    }

    private async Task<Unit> AddRow()
    {
        try
        {
            int lastId;
            switch (SelectedIndex)
            {
                case 0:
                {
                    lastId = Items.Last().Id+1;
                    Items.Add(new Item()
                    {
                        Id = lastId,
                        Description = "",
                        ItemType = ItemTypes.First(),
                        Name = "",
                        Price = 0,
                        Record = Records.First(),
                        ToDate =new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                    });
                    break;
                }
                case 1:
                {
                    lastId = Records.Last().Id+1;
                    Records.Add(new Record()
                    {
                        Id = lastId
                    });
                    break;
                }
                case 2:
                {
                    lastId = ItemTypes.Last().Id+1;
                    ItemTypes.Add(new  ItemType()
                    {
                        Id = lastId
                    });
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorText = ex.Message;
            ErrorOpacity = 0.5;
            Task.Run(HideError);
        }
        return Unit.Default;
    }
    
    #endregion

    public ControlWindowViewModel()
    {
        ButtonSearchVisibility = true;
        SelectedIndex = 0;
        SearchText = "";
        SearchLabelText = "";
        Items = new ObservableCollection<Item>(DatabaseInterface.Items);
        Records = new ObservableCollection<Record>(DatabaseInterface.Records);
        ItemTypes = new ObservableCollection<ItemType>(DatabaseInterface.ItemTypes);
        LoadDataCommand = ReactiveCommand.CreateFromTask(LoadData);
        SearchCommand = ReactiveCommand.CreateFromTask(Search);
        SearchByDateCommand = ReactiveCommand.CreateFromTask(SearchByDate);
        AddRowCommand = ReactiveCommand.CreateFromTask(AddRow);
    }
}