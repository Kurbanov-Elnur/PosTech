using PostTech.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace PostTech.ViewModels;

internal class LoginViewModel : BindableBase
{
    private readonly IUserService _userService;
    private readonly INavigationService _navigationService;

    private bool _isPasswordError;
    public bool IsPasswordError
    {
        get => _isPasswordError;
        set => SetProperty(ref _isPasswordError, value);
    }

    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }
    
    private string _userName;
    public string Username
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public string Password { get; set; } = "";

    public LoginViewModel(IUserService userService, INavigationService navigationService)
    {
        _userService = userService;
        _navigationService = navigationService;

        Login = new DelegateCommand(async () =>
        {
            try
            {
                //await _userService.Login(Username, Password);
                //IsPasswordError = false;
                //ErrorMessage = string.Empty;

                _navigationService.NavigateTo<WorkspaceViewModel>();
            }
            catch (Exception e)
            {
                IsPasswordError = true;
                ErrorMessage = e.Message;

                await Task.Delay(1000);
                IsPasswordError = false;
            }
        });
    }

    public DelegateCommand Login { get; private set; }
}