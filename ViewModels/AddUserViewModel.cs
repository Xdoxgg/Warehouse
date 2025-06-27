using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class AddUserViewModel : ViewModelBase
{
    private double _errorOpacity;
    private string _errorText;

    public double ErrorOpacity
    {
        get => _errorOpacity;
        set => this.RaiseAndSetIfChanged(ref _errorOpacity, value);
    }

    public string ErrorText
    {
        get => _errorText;
        set => this.RaiseAndSetIfChanged(ref _errorText, value);
    }


    public async Task<Unit> HideError()
    {
        Thread.Sleep(3000);
        ErrorText = "";
        ErrorOpacity = 0;
        return Unit.Default;
    }
    
    private ReactiveCommand<Unit, Unit> _buttonClickCommand;

    public ReactiveCommand<Unit, Unit> ButtonClickCommand
    {
        get=>  _buttonClickCommand;
        set => _buttonClickCommand = value;
    }

    private string _name;

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    private string _password;

    public string Password
    {
        get=> _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }


    private async Task<Unit> AddUser()
    {
        try
        {
            DatabaseInterface.AddUser(Name, Password, false);
            // (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow?.Close();
        }
        catch (Exception ex)
        {
            ErrorOpacity = 0.5;
            ErrorText = ex.Message;
            Task.Run(HideError);
        }

        return Unit.Default;
    }
    
    public AddUserViewModel()
    {
        ErrorOpacity = 0;
        ErrorText = "";
        ButtonClickCommand = ReactiveCommand.CreateFromTask(AddUser);
    }
}