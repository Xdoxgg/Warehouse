using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class RecordsGridUserControl : UserControl
{
    public RecordsGridUserControl()
    {
        InitializeComponent();
        DataContext = new RecordsGridUserControlViewModel();
    }
}