using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Warehouse.ViewModels;

namespace Warehouse.Views;

public partial class ControlWindow : Window
{
    public ControlWindow()
    {
        InitializeComponent();
        DataContext = new ControlWindowViewModel();
        DataTable.DataContext = new ControlWindowViewModel();
        ComboBoxMenu.SelectedIndex = 0;
    }

    private void LoadItemsDataColumns()
    {
        DataTable.Columns.Clear();
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Id",
            Binding = new Binding("Id")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Название",
            Binding = new Binding("Name")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Цена",
            Binding = new Binding("Price")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Описание",
            Binding = new Binding("Description")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Годен до",
            Binding = new Binding("ToDate")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Категория",
            Binding = new Binding("ItemType.Name")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Прибыл",
            Binding = new Binding("Record.DateEntrance")
        });
        DataTable.Bind(DataGrid.ItemsSourceProperty, new Binding("Items"));
    }

    private void LoadRecordsDataColumns()
    {
        DataTable.Columns.Clear();
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Id",
            Binding = new Binding("Id")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Дата прибытия",
            Binding = new Binding("DateEntrance")
        });

        DataTable.Bind(DataGrid.ItemsSourceProperty, new Binding("Records"));
    }

    private void LoadItemTypesDataColumns()
    {
        DataTable.Columns.Clear();
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Id",
            Binding = new Binding("Id")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Название",
            Binding = new Binding("Name")
        });
        DataTable.Columns.Add(new DataGridTextColumn
        {
            Header = "Описание",
            Binding = new Binding("Description")
        });

        DataTable.Bind(DataGrid.ItemsSourceProperty, new Binding("ItemTypes"));
    }

    private void ComboBoxMenu_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        switch ((sender as ComboBox)?.SelectedIndex)
        {
            case 0:
            {
                LoadItemsDataColumns();
                break;
            }
            case 1:
            {
                LoadRecordsDataColumns();
                break;
            }
            case 2:
            {
                LoadItemTypesDataColumns();
                break;
            }
        }

        ((ControlWindowViewModel)DataContext).LoadDataCommand.Execute();
    }

    private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ((ControlWindowViewModel)DataContext).SearchCommand.Execute();
        DataTable.SelectedItem = ((ControlWindowViewModel)DataContext).SelectedDataGridItem;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        ComboBoxMenu_OnSelectionChanged(ComboBoxMenu,null);
    }
}