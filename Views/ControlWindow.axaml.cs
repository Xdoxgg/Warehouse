using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class ControlWindow : Window
{
    public ControlWindow()
    {
        InitializeComponent();
        DataContext = new ControlWindowViewModel();
    }

 
}