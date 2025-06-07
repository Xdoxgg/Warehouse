using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class RecordsGridUserControlViewModel:ViewModelBase
{
    private ObservableCollection<Record> _records;

    public ObservableCollection<Record> Records
    {
        get => _records;
        set => this.RaiseAndSetIfChanged(ref _records, value);
    }

    public RecordsGridUserControlViewModel()
    {
        _records = new ObservableCollection<Record>(DatabaseInterface.Records);
        Console.WriteLine(_records.Count);
    }
}