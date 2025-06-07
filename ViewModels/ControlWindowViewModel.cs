using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Warehouse.Models;
using Warehouse.Views;

namespace Warehouse.ViewModels;

public class ControlWindowViewModel: ViewModelBase
{
    private object? _userControl;

    public object? UserControl
    {
        get => _userControl;
        set => this.RaiseAndSetIfChanged(ref _userControl, value);
    }
   
    public ControlWindowViewModel()
    {
        // UserControl = new ItemsGridUserControl();
        UserControl = new RecordsGridUserControl();

    }

 
 

}