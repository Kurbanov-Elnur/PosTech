using GalaSoft.MvvmLight.Messaging;
using PosTech.Services.Classes;
using PosTech.Services.Interfaces;
using PostTech.Data.Contexts;
using PostTech.Services.Classes;
using PostTech.Services.Interfaces;
using PostTech.ViewModels;
using PostTech.Views;
using SimpleInjector;
using System.Windows;

namespace PostTech;

public partial class App : Application
{
    public static Container Container { get; set; } = new();

    public void Register()
    {
        Container.RegisterSingleton<PostAppContext>();

        Container.RegisterSingleton<IMessenger, Messenger>();

        Container.RegisterSingleton<INavigationService, NavigationService>();
        Container.RegisterSingleton<IDataService, DataService>();
        Container.RegisterSingleton<IUserService, UserService>();
        Container.RegisterSingleton<IStoresService, StoresService>();

        Container.RegisterSingleton<MainViewModel>();
        Container.RegisterSingleton<LoginViewModel>();
        Container.RegisterSingleton<WorkspaceViewModel>();
        Container.RegisterSingleton<StoreViewModel>();
        Container.RegisterSingleton<UsersViewModel>();

        Container.Verify();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Register();

        MainView window = new();

        window.DataContext = Container.GetInstance<MainViewModel>();

        window.ShowDialog();
    }
}