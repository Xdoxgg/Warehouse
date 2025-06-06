using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class ItemsGridUserControl : UserControl
{
    public ItemsGridUserControl()
    {
        InitializeComponent();
        DataContext = new ItemsGridUCViewModel();
    }
}