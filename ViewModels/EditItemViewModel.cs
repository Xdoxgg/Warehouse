using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class EditItemViewModel : ViewModelBase
{
    public ObservableCollection<Record> Records { get; }
    public ObservableCollection<ItemType> ItemTypes { get; }
    private Item _editItem;
    private readonly Item _original;
    private ReactiveCommand<Unit, Unit> _saveCommand;
    private ReactiveCommand<Unit, Unit> _cancelCommand;
    private ReactiveCommand<Unit, Unit> _clearDateCommand;
    private ItemType _selectedType;
    private Record _selectedRecord;
    private DateTimeOffset _viewTime;
    private bool _withToDate;

    public bool WithToDate
    {
        get => _withToDate;
        set => this.RaiseAndSetIfChanged(ref _withToDate, value);
    }

    public DateTimeOffset ViewTime
    {
        get => _viewTime;
        set => this.RaiseAndSetIfChanged(ref _viewTime, value);
    }

    public Record SelectedRecord
    {
        get => _selectedRecord;
        set => this.RaiseAndSetIfChanged(ref _selectedRecord, value);
    }

    public ItemType SelectedType
    {
        get => _selectedType;
        set => this.RaiseAndSetIfChanged(ref _selectedType, value);
    }

    public ReactiveCommand<Unit, Unit> SaveCommand
    {
        get => _saveCommand;
        set => _saveCommand = value;
    }

    public ReactiveCommand<Unit, Unit> CancelCommand
    {
        get => _cancelCommand;
        set => _cancelCommand = value;
    }

    public ReactiveCommand<Unit, Unit> ClearDateCommand
    {
        get => _clearDateCommand;
        set => _clearDateCommand = value;
    }

    public Item EditItem
    {
        get => _editItem;
        set => this.RaiseAndSetIfChanged(ref _editItem, value);
    }

    private async Task<Unit> Save()
    {
        _original.Description = EditItem.Description;
        _original.Price = EditItem.Price;
        _original.Name = EditItem.Name;
        if (WithToDate)
        {
            _original.ToDate = new DateOnly(ViewTime.Date.Year, ViewTime.Date.Month, ViewTime.Date.Day);
        }
        else
        {
            _original.ToDate = null;
        }

        _original.ItemType = SelectedType;
        _original.Record = SelectedRecord;
        _original.IsReverted = EditItem.IsReverted;
        _original.IsSend = EditItem.IsSend;
        await _updater();
        return Unit.Default;
    }

    private Func<Task> _updater;

    public EditItemViewModel(Item item, Func<Task> onClose, Func<Task> onDataGridUpdate)
    {
        _updater = onDataGridUpdate;
        _original = item;
        ItemTypes = new ObservableCollection<ItemType>(DatabaseInterface.ItemTypes);
        Records = new ObservableCollection<Record>(DatabaseInterface.Records);
        EditItem = item;
        EditItem.IsReverted = item.IsReverted;
        EditItem.IsSend = item.IsSend;
        SaveCommand = ReactiveCommand.CreateFromTask(Save);
        CancelCommand = ReactiveCommand.CreateFromTask(onClose);

        if (item.ItemType != null)
        {
            SelectedType = ItemTypes.First(el => el.Id == item.ItemType.Id);
        }

        if (item.Record != null)
        {
            SelectedRecord = Records.First(el => el.Id == item.Record.Id);
        }

        try
        {
            ViewTime = new DateTimeOffset(new DateTime(item.ToDate.Value.Year, item.ToDate.Value.Month,
                item.ToDate.Value.Day));
            WithToDate = true;
        }
        catch (Exception ex)
        {
            ViewTime = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            WithToDate = false;
        }

        ClearDateCommand = ReactiveCommand.CreateFromTask(async () => { ViewTime = new DateTimeOffset(); });
    }
}