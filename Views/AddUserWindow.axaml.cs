using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class AddUserWindow : Window
{
    public AddUserWindow()
    {
        InitializeComponent();
        DataContext = new AddUserViewModel();
    }
}