using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public class LoginFormViewModel : ViewModelBase
{
    LoginFormModel _model = new LoginFormModel();
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
        ErrorOpacity = 0;
        ErrorText = "";
        ButtonClickCommand = ReactiveCommand.CreateFromTask(OnButtonClick);
    }

    public async Task<Unit> OnButtonClick()
    {
        // Проверка на пустые поля
        if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Name))
        {
            ErrorText = "Поле должно быть заполнено";
            ErrorOpacity = 0.5;
            Task.Run( HideError);
            return Unit.Default; // Выход из метода, если поля пустые
        }

        try
        {
            var existed = _model.ExistUser(Name, Password);
            if (existed)
            {
                ErrorText = "Успех";
                ErrorOpacity = 0.5;
                Task.Run( HideError);
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