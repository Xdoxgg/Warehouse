using Avalonia.Controls;
using Warehouse.ViewModels;
namespace Warehouse.Views;

public partial class ControlWindow : Window
{
    public ControlWindow()
    {
        InitializeComponent();
        DataContext = new ControlWindowViewModel();
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