using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class Q2VM:ViewModelBase
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
    private DateTimeOffset _toDate;
    public DateTimeOffset ToDate
    {
        get => _toDate;
        set => this.RaiseAndSetIfChanged(ref _toDate, value);
    }
    private DateTimeOffset _fromDate;
    public DateTimeOffset FromDate
    {
        get => _fromDate;
        set => this.RaiseAndSetIfChanged(ref _fromDate, value);
    }

    private ReactiveCommand<Unit, Unit> _createCommand;

    public ReactiveCommand<Unit, Unit> CreateCommand
    {
        get=> _createCommand;
        set => this.RaiseAndSetIfChanged(ref _createCommand, value);
    }

    private async Task<Unit> Create()
    {
        try
        {
            var fromDate = new DateOnly(FromDate.Date.Year, FromDate.Date.Month, FromDate.Date.Day);
            var toDate = new DateOnly(ToDate.Date.Year, ToDate.Date.Month, ToDate.Date.Day);
            List<Item> list = DatabaseInterface.Items.Where(el =>
                el.IsSend && (el.Record.DateEntrance > fromDate && el.Record.DateEntrance < toDate)).ToList();
            ReportGenerator.GenerateDefaultReport(list);
        }
        catch (Exception ex)
        {
            ErrorText = ex.Message;
            ErrorOpacity = 0.5;
            Task.Run(HideError);

        }

        return Unit.Default;
    }
    public Q2VM()
    {
        ErrorOpacity = 0;
        ErrorText = "";
        CreateCommand =ReactiveCommand.CreateFromTask(Create);
    }
}