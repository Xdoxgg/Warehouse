using System.Reactive;
using Warehouse.Models;
using System.Threading.Tasks;
using ReactiveUI;

namespace Warehouse.ViewModels;

public class RegistrationFromViewModel : ViewModelBase
{
    private RegistrationFormModel _model;
    private string _inputName;
    private string _inputPassword1;
    private string _inputPassword2;

    public string InputName
    {
        get => _inputName;
        set => this.RaiseAndSetIfChanged(ref _inputName, value);
    }

    public string InputPassword1
    {
        get => _inputPassword1;
        set => this.RaiseAndSetIfChanged(ref _inputPassword1, value);
    }

    public string InputPassword2
    {
        get => _inputPassword2;
        set => this.RaiseAndSetIfChanged(ref _inputPassword2, value);
    }

    private ReactiveCommand<Unit, Unit> _buttonClickCommand;

    public ReactiveCommand<Unit, Unit> ButtonClickCommand
    {
        get => _buttonClickCommand;
        set => _buttonClickCommand = value;
    }

    private int _errorOpacity;

    public int ErrorOpacity
    {
        get => _errorOpacity;
        set => this.RaiseAndSetIfChanged(ref _errorOpacity, value);
    }

    private string _errorText;

    public string ErrorText
    {
        get => _errorText;
        set => this.RaiseAndSetIfChanged(ref _errorText, value);
    }

    public RegistrationFromViewModel()
    {
        _errorOpacity = 0;
        _errorText = "";
        _model = new RegistrationFormModel();
        _buttonClickCommand = ReactiveCommand.CreateFromTask(OnButtonClick);
    }

    private async Task<Unit> OnButtonClick()
    {
        if (_inputPassword1 == string.Empty || _inputPassword2 == string.Empty || _inputName == string.Empty)
        {
            _errorText = "Вводимые параметры не могут быть пустыми";
            _errorOpacity = 1;
            return Unit.Default;
        }

        _model.CheckPasswords(_inputPassword1, _inputPassword2);
        return Unit.Default;
    }
}