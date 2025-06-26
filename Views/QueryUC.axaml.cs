using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class QueryUC : UserControl
{
    public QueryUC()
    {
        InitializeComponent();
        DataContext = new QueryWM();
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        (DataContext as QueryWM).Load();
    }
}