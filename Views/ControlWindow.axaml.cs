using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Warehouse.Views;

public partial class ControlWindow : Window
{
    public ControlWindow()
    {
        InitializeComponent();
        this.Closing += OnClosing;
    }
    private void OnClosing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        var lifetime = Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        lifetime?.Shutdown();
    }
}