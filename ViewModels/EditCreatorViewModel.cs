using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class EditCreatorViewModel:ViewModelBase
{
    private Creator _type;
    private Creator _original;

    public Creator Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
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
        _original.Name  = Type.Name;
        await _updater();
        return Unit.Default;
    }
    
 
    public EditCreatorViewModel(Creator record, Func<Task> onClose, Func<Task> onDataGridUpdate)
    {
        _updater = onDataGridUpdate;
        _original = record;
        Type = record;
        SaveCommand = ReactiveCommand.CreateFromTask(Save);
        CancelCommand = ReactiveCommand.CreateFromTask(onClose);
        SaveCommand.ThrownExceptions
            .Subscribe(ex => { Console.WriteLine($"Ошибка в SaveCommand: {ex.Message}"); });
        
    }
}