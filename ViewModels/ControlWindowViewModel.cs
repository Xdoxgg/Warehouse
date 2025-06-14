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
        ObservableCollection<object> newDataGridItems;

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
            default:
                return Unit.Default;
        }

        DataGridItems = newDataGridItems; // Это вызовет RaisePropertyChanged автоматически
        return Unit.Default;
    }

    private async Task<Unit> Search()
    {
        switch (SelectedIndex)
        {
            case 0:
            {
                var result = DatabaseInterface.Items.First(x =>
                    x.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase));
                SelectedDataGridItem = result;
                break;
            }
            // type record search at SearchByDate() method
            case 2:
            {
                var result =
                    DatabaseInterface.ItemTypes.First(x =>
                        x.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase));
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

    private async Task<Unit> AddRow()
    {
        try
        {
            switch (SelectedIndex)
            {
                case 0:
                {
                    Item? lastItem = DataGridItems.Last() as Item;
                    
                    // lastId = DatabaseInterface.Items.Last().Id + 1;
                    if (lastItem != null)
                        DataGridItems.Add(new Item()
                        {
                            Id = lastItem.Id + 1,
                            Description = "",
                            ItemType = DatabaseInterface.ItemTypes.OrderBy(el => el.Id).FirstOrDefault(),
                            Name = "",
                            Price = 0,
                            Record = DatabaseInterface.Records.OrderBy(el => el.DateEntrance).LastOrDefault(),
                            ToDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                        });
                    break;
                }
                case 1:
                {
                    // lastId = Records.Last().Id+1;
                    // Records.Add(new Record()
                    // {
                    //     Id = lastId
                    // });
                    break;
                }
                case 2:
                {
                    // lastId = ItemTypes.Last().Id+1;
                    // ItemTypes.Add(new  ItemType()
                    // {
                    //     Id = lastId
                    // });
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
        AddRowCommand = ReactiveCommand.CreateFromTask(AddRow);
    }
}