using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using ReactiveUI;
using Warehouse.Models;
using Warehouse.Views;

namespace Warehouse.ViewModels;

public class LoginFormViewModel : ViewModelBase
{

    private string _name;

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _password;

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    private double _errorOpacity;

    public double ErrorOpacity
    {
        get => _errorOpacity;
        set => this.RaiseAndSetIfChanged(ref _errorOpacity, value);
    }

    private string _errorText;
    private ReactiveCommand<Unit, Unit> _buttonClickCommand;

    public string ErrorText
    {
        get => _errorText;
        set => this.RaiseAndSetIfChanged(ref _errorText, value);
    }

    public ReactiveCommand<Unit, Unit> ButtonClickCommand
    {
        get => _buttonClickCommand;
        set => _buttonClickCommand = value;
    }

    public LoginFormViewModel()
    {
        Name = "";
        Password = "";
        ErrorOpacity = 0;
        ErrorText = "";
        ButtonClickCommand = ReactiveCommand.CreateFromTask(OnButtonClick);
    }

    public async Task<Unit> OnButtonClick()
    {

        if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Name))
        {
            ErrorText = "Поле должно быть заполнено";
            ErrorOpacity = 0.5;
            Task.Run( HideError);
            return Unit.Default; 
        }

        try
        {
            var existed = DatabaseInterface.ExistUser(Name, Password);
            if (existed)
            {
                
                new ControlWindow(DatabaseInterface.UserType(Name,Password)).Show();
                var window = (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                if (window != null)
                {
                    window.Close();
                }
                return Unit.Default;
            }
            else
            {
                ErrorText = "Такого пользователя не существует";
                ErrorOpacity = 0.5;
                Task.Run( HideError);
                return Unit.Default;
            }
        }
        catch (Exception ex)
        {
            ErrorText = ex.Message;
            ErrorOpacity = 0.5;
            Task.Run( HideError);
        }

        return Unit.Default;
    }

    public async Task<Unit> HideError()
    {
        Thread.Sleep(3000);
        ErrorText = "";
        ErrorOpacity = 0;
        return Unit.Default;
    }
}