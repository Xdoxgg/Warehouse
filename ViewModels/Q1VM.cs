using System;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class Q1VM:ViewModelBase
{
    private double _errorOpacity;
    private string _errorText;

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

    private ReactiveCommand<Unit, Unit> _create;

    public ReactiveCommand<Unit, Unit> Create
    {
        get => _create;
        set => _create = value;
    }

    private async Task<Unit> CreateFunc()
    {
        try
        {
            Task.Run(() =>
            {
                ReportGenerator.GenerateDefaultReport(DatabaseInterface.Items.Where(el=>!el.IsSend).ToList());
            });
        }
        catch (Exception ex)
        {
            ErrorText = ex.Message;
            ErrorOpacity = 0.5;
            Task.Run(HideError);

        }
        
        
        return Unit.Default;
    }

    public Q1VM()
    {
        ErrorOpacity = 0;
        ErrorText = "";
        Create = ReactiveCommand.CreateFromTask(CreateFunc);
    }
}