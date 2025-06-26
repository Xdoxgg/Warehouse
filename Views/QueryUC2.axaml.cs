using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class QueryUC2 : UserControl
{
    public QueryUC2()
    {
        InitializeComponent();
        DataContext = new Q2VM();
    }
}