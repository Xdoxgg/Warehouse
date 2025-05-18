using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerPressed(object? sender, PointerEventArgs e)
    {
        e.Handled = false;
        PasswordTextBox.PasswordChar = '\0'; // Показываем пароль
    }

    private void InputElement_OnPointerReleased(object? sender, PointerEventArgs e)
    {
        PasswordTextBox.PasswordChar = '*'; // Восстанавливаем маскировку пароля
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as MainWindowViewModel).ButtonClickCommand.Execute();
    }
}