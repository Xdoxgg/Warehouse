using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class Q3VM : ViewModelBase
{
    private double _errorOpacity;
    private string _errorText;

    public async Task<Unit> HideError()
    {
        Thread.Sleep(3000);
        ErrorText = "";
        ErrorOpacity = 0;
        return Unit.Default;
    }

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

    private Creator _selectedCreator;

    public Creator SelectedCreator
    {
        get => _selectedCreator;
        set => this.RaiseAndSetIfChanged(ref this._selectedCreator, value);
    }

    private ObservableCollection<Creator> _creatrs;

    public ObservableCollection<Creator> Creators
    {
        get => _creatrs;
        set => _creatrs = value;
    }
    
    private ReactiveCommand<Unit, Unit> _createCommand;

    public ReactiveCommand<Unit, Unit> CreateCommand
    {
        get => _createCommand;
        set => _createCommand = value;
    }

    private async Task<Unit> Create()
    {
        if (SelectedCreator is null)
        {
            ErrorText = "Выберите поставщика";
            ErrorOpacity = 0.5;
            Task.Run(HideError);
            return Unit.Default;

        }
        var res = DatabaseInterface.Items.Where(el=>el.CreatorId == SelectedCreator.Id).ToList();
        ReportGenerator.GenerateDefaultReport(res);
        return Unit.Default;
    }

    public Q3VM()
    {
        ErrorOpacity = 0;
        ErrorText = "";
        Creators = new ObservableCollection<Creator>(DatabaseInterface.Creators);
        CreateCommand = ReactiveCommand.CreateFromTask(Create);
    }
}