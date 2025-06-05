using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class ControlWindowViewModel: ViewModelBase
{
    private ObservableCollection<Item> _items;
    public ObservableCollection<Item> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }
   
    public ControlWindowViewModel()
    {
   
        Items = new ObservableCollection<Item>(DatabaseInterface.Items);
    }

   public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public Person(string firstName , string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
 

}