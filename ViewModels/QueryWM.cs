using ReactiveUI;

namespace Warehouse.ViewModels;

public class QueryWM:ViewModelBase
{
    private int _selectedIndex;
    private ViewModelBase _selectedItem;

    public int SelectedIndex
    {
        get => _selectedIndex;
        set=> this.RaiseAndSetIfChanged(ref _selectedIndex, value);
    }

    public ViewModelBase SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    public void Load()
    {
        switch (_selectedIndex)
        {
            case 0:
            {
                SelectedItem = new Q1VM();
                break;
            }
            case 1:
            {
                SelectedItem = new Q2VM();
                break;
            }
            case 2:
            {
                SelectedItem = new Q3VM();
                break;
            }
        }
    }
    
    public QueryWM()
    {
        _selectedIndex = 0;
        Load();
    }
}