using GalaSoft.MvvmLight.Messaging;
using PosTech.Messages;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.ViewModels;

class MainViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private BindableBase currentView;

    public BindableBase CurrentView
    {
        get => currentView;
        set
        {
            SetProperty(ref currentView, value);
        }
    }

    public MainViewModel(IMessenger messenger)
    {
        _messenger = messenger;
        CurrentView = App.Container.GetInstance<LoginViewModel>();

        _messenger.Register<NavigationMessage>(this, message =>
        {
            if (message.ViewModelType != null)
                CurrentView = message.ViewModelType;
        });
    }
}