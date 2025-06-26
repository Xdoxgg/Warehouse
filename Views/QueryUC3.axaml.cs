using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class QueryUC3 : UserControl
{
    public QueryUC3()
    {
        InitializeComponent();
        DataContext = new Q3VM();
    }
}