using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;
using Warehouse.Views;

namespace Warehouse.ViewModels;

public class ControlWindowViewModel : ViewModelBase
{
    #region Properties

    private bool _type;

    public bool Type
    {
        get=> _type;
        set=> this.RaiseAndSetIfChanged(ref _type,value);
    }
    
    private RefresherDelegate _refresher;

    private ViewModelBase _editViewModel;

    public ViewModelBase EditViewModel
    {
        get => _editViewModel;
        set => this.RaiseAndSetIfChanged(ref _editViewModel, value);
    }


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

    #region Commands

    private ReactiveCommand<Unit, Unit> _addUserCommand;

    public ReactiveCommand<Unit, Unit> AddUserCommand
    {
        get => _addUserCommand;
        set => _addUserCommand = value;
    }

    private ReactiveCommand<Unit, Unit> _openEditCommand;

    public ReactiveCommand<Unit, Unit> OpenEditCommand
    {
        get => _openEditCommand;
        set => _openEditCommand = value;
    }


    private ReactiveCommand<Unit, Unit> _saveCommand;

    public ReactiveCommand<Unit, Unit> SaveCommand
    {
        get => _saveCommand;
        set => _saveCommand = value;
    }

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

    private async Task<Unit> AddRow()
    {
        switch (SelectedIndex)
        {
            case 0:
                DataGridItems.Add(new Item
                {
                    Id = (DataGridItems.Last() != null) ? (DataGridItems.Last() as Item).Id + 1 : 1,
                    Description = "",
                    Name = "",
                    ItemType = null,
                    Record = null,
                    Price = 0
                });
                break;
            case 1:
                DataGridItems.Add(new Record()
                {
                    Id = (DataGridItems.Last() != null) ? (DataGridItems.Last() as Record).Id + 1 : 1,
                    DateEntrance = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                });
                break;
            case 2:
                DataGridItems.Add(new ItemType()
                {
                    Id = (DataGridItems.Last() != null) ? (DataGridItems.Last() as ItemType).Id + 1 : 1,
                    Description = "",
                    Name = "",
                });
                break;
            case 3:
                try
                {
                    DataGridItems.Add(new Creator()
                    {
                        Id = (DataGridItems.Last() != null) ? (DataGridItems.Last() as Creator).Id + 1 : 1,
                        Name = "",
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
            default:
                return Unit.Default;
        }

        // this.RaisePropertyChanged(nameof(DataGridItems));
        return Unit.Default;
    }


    private async Task<Unit> LoadData()
    {
        ObservableCollection<object> newDataGridItems;
        await EditOnClose();
        var vm = new StatisticViewModel();
        EditViewModel = vm;
        _refresher = vm.RefreshStatistic;
        this.RaisePropertyChanged(nameof(DataGridItems));
        switch (SelectedIndex)
        {
            case 0:
                ButtonSearchVisibility = false;
                newDataGridItems = new ObservableCollection<object>(DatabaseInterface.Items);
                SearchLabelText = "Введите название";
                break;
            case 1:
                ButtonSearchVisibility = true;
                newDataGridItems = new ObservableCollection<object>(DatabaseInterface.Records);
                SearchLabelText = "Введите дату прибытия";
                break;
            case 2:
                ButtonSearchVisibility = false;
                newDataGridItems = new ObservableCollection<object>(DatabaseInterface.ItemTypes);
                SearchLabelText = "Введите название";
                break;
            case 3:
            {
                ButtonSearchVisibility = false;
                newDataGridItems = new ObservableCollection<object>(DatabaseInterface.Creators);
                SearchLabelText = "Введите название";
                break;
            }
            default:
                return Unit.Default;
        }

        _refresher();
        DataGridItems = newDataGridItems;
        return Unit.Default;
    }

    private async Task<Unit> Search()
    {
        switch (SelectedIndex)
        {
            case 0:
            {
                var result = DatabaseInterface.Items.First(x =>
                    x.Name.ToLower().Contains(SearchText.ToLower()));
                SelectedDataGridItem = result;
                break;
            }
            // type record search at SearchByDate() method
            case 2:
            {
                var result =
                    DatabaseInterface.ItemTypes.First(x =>
                        x.Name.ToLower().Contains(SearchText.ToLower()));
                SelectedDataGridItem = result;
                break;
            }
            case 3:
            {
                var result =
                    DatabaseInterface.Creators.First(x =>
                        x.Name.ToLower().Contains(SearchText.ToLower()));
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
            var result = DatabaseInterface.Records.First(x => x.DateEntrance == timeSearch);
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


    private async Task<Unit> Save()
    {
        try
        {
            switch (SelectedIndex)
            {
                case 0:
                {
                    DatabaseInterface.SaveOrUpdateItems(DataGridItems);
                    break;
                }
                case 1:
                {
                    DatabaseInterface.SaveOrUpdateRecords(DataGridItems);
                    break;
                }
                case 2:
                {
                    DatabaseInterface.SaveOrUpdateItemTypes(DataGridItems);
                    break;
                }
                case 3:
                {
                    DatabaseInterface.SaveOrUpdateCreators(DataGridItems);
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        await LoadData();
        return Unit.Default;
    }

    private async Task<Unit> EditOnClose()
    {
        this.RaisePropertyChanged(nameof(DataGridItems));
        var vm = new StatisticViewModel();
        EditViewModel = vm;
        _refresher = vm.RefreshStatistic;
        return Unit.Default;
    }


    private async Task<Unit> RefreshTable()
    {
        ObservableCollection<object> newDataGridItems = new ObservableCollection<object>();
        foreach (var item in DataGridItems)
        {
            newDataGridItems.Add(item);
        }

        DataGridItems.Clear();
        DataGridItems = newDataGridItems;
        return Unit.Default;
    }

    private async Task<Unit> OpenEditor()
    {
        if (SelectedDataGridItem != null)
        {
            switch (SelectedIndex)
            {
                case 0:
                {
                    try
                    {
                        var t = SelectedDataGridItem as Item;
                        EditViewModel = new EditItemViewModel(SelectedDataGridItem as Item, EditOnClose, RefreshTable);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                }
                case 1:
                {
                    EditViewModel = new EditRecordViewModel(SelectedDataGridItem as Record, EditOnClose, RefreshTable);
                    break;
                }
                case 2:
                {
                    EditViewModel = new EditItemTypeViewModel(SelectedDataGridItem as ItemType, EditOnClose,
                        RefreshTable);

                    break;
                }
                case 3:
                {
                    EditViewModel = new EditCreatorViewModel(SelectedDataGridItem as Creator, EditOnClose,
                        RefreshTable);
                    break;
                }
            }

            this.RaisePropertyChanged(nameof(EditViewModel));
        }

        return Unit.Default;
    }

    private async Task<Unit> LoadAddUserWndow()
    {
        new AddUserWindow().Show();
        return Unit.Default;
    }
    
    
    #endregion

    #region DataGridCollections

    private ObservableCollection<object> _dataGridItems;

    public ObservableCollection<object> DataGridItems
    {
        get => _dataGridItems;

        set
        {
            _dataGridItems = value;
            this.RaisePropertyChanged(nameof(DataGridItems));
        }
    }

    #endregion

    public ControlWindowViewModel()
    {
        ButtonSearchVisibility = true;
        SelectedIndex = 0;
        SearchText = "";
        SearchLabelText = "";
        LoadDataCommand = ReactiveCommand.CreateFromTask(LoadData);
        SearchCommand = ReactiveCommand.CreateFromTask(Search);
        SearchByDateCommand = ReactiveCommand.CreateFromTask(SearchByDate);
        SaveCommand = ReactiveCommand.CreateFromTask(Save);
        OpenEditCommand = ReactiveCommand.CreateFromTask(OpenEditor);
        AddRowCommand = ReactiveCommand.CreateFromTask(AddRow);
        AddUserCommand=ReactiveCommand.CreateFromTask(LoadAddUserWndow);
    }
}