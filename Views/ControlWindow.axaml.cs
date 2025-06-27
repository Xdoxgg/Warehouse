using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Data;
using Warehouse.Models;
using Warehouse.ViewModels;
namespace Warehouse.Views;

public partial class ControlWindow : Window
{
    public ControlWindow(bool type)
    {
        InitializeComponent();
        DataContext = new ControlWindowViewModel();
        (DataContext as ControlWindowViewModel).Type = type;
        ComboBoxMenu.SelectedIndex = 0;
    }



    private void ComboBoxMenu_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
 
        ((ControlWindowViewModel)DataContext).LoadDataCommand.Execute();
    }

    private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ((ControlWindowViewModel)DataContext).SearchCommand.Execute();
        DataTable.SelectedItem = ((ControlWindowViewModel)DataContext).SelectedDataGridItem;
    }



}