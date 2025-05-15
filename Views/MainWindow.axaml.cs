using System;
using Avalonia.Controls;
using Avalonia.Input;
using Warehouse.Models;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
    
    
}