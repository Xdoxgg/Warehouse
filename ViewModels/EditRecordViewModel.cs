using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class EditRecordViewModel : ViewModelBase
{
    private Record _record;
    private Record _original;

    public Record Record
    {
        get => _record;
        set => this.RaiseAndSetIfChanged(ref _record, value);
    }

    private Func<Task> _updater;

    private ReactiveCommand<Unit, Unit> _saveCommand;
    private ReactiveCommand<Unit, Unit> _cancelCommand;

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

    private async Task<Unit> Save()
    {
        _original.DateEntrance =new DateOnly(ViewTime.Date.Year, ViewTime.Date.Month, ViewTime.Date.Day);
        await _updater();
        return Unit.Default;
    }
    
    private DateTimeOffset _viewTime;

    public DateTimeOffset ViewTime
    {
        get => _viewTime;
        set => this.RaiseAndSetIfChanged(ref _viewTime, value);
    }

    public EditRecordViewModel(Record record, Func<Task> onClose, Func<Task> onDataGridUpdate)
    {
        _updater = onDataGridUpdate;
        _original = record;
        SaveCommand = ReactiveCommand.CreateFromTask(Save);
        CancelCommand = ReactiveCommand.CreateFromTask(onClose);
        SaveCommand.ThrownExceptions
            .Subscribe(ex => { Console.WriteLine($"Ошибка в SaveCommand: {ex.Message}"); });
        ViewTime = new DateTimeOffset(new DateTime(record.DateEntrance.Year,record.DateEntrance.Month, record.DateEntrance.Day));

    }
}