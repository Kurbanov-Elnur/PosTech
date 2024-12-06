using GalaSoft.MvvmLight.Messaging;
using PosTech.Services.Classes;
using PosTech.Services.Interfaces;
using PosTech.ViewModels;
using PosTech.Data.Contexts;
using PosTech.Views;
using SimpleInjector;
using System.Windows;

namespace PosTech;

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
        Container.RegisterSingleton<ICompanyService, CompanyService>();

        Container.RegisterSingleton<MainViewModel>();
        Container.RegisterSingleton<LoginViewModel>();
        Container.RegisterSingleton<WorkspaceViewModel>();
        Container.RegisterSingleton<StoreViewModel>();
        Container.RegisterSingleton<CompanyViewModel>();
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