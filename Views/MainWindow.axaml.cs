using System;
using System.Security.Cryptography;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Warehouse.Models;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        if (!DatabaseInterface.ExistUser("test", "test"))
        {
            DatabaseInterface.AddUser("test", "test");
        }
    }


}