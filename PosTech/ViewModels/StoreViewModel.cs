using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualBasic.Logging;
using PosTech.Data.Models;
using PosTech.Messages;
using PosTech.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosTech.ViewModels;

class StoreViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IStoresService _storesService;
    private readonly ICompanyService _companyService;
    private readonly IMessenger _messenger;

    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    private Store _editableStore;
    public Store EditableStore
    {
        get => _editableStore;
        set => SetProperty(ref _editableStore, value);
    }

    private Store _selectedStore;
    public Store SelectedStore
    {
        get => _selectedStore;
        set
        {
            SetProperty(ref _selectedStore, value);
            if (_selectedStore != null)
            {
                EditableStore = _selectedStore;
            }
        }
    }

    private Company _selectedCompany;
    public Company SelectedCompany
    {
        get => _selectedCompany;
        set
        {
            SetProperty(ref _selectedCompany, value);
            if (EditableStore != null && _selectedCompany != null)
            {
                EditableStore.CompanyId = _selectedCompany.Id;
                EditableStore.Company = _selectedCompany;
            }
        }
    }

    public ObservableCollection<Store> Stores { get; set; } = new();
    public ObservableCollection<Company> Companies { get; set; } = new();

    public StoreViewModel(INavigationService navigationService, IStoresService storesService, ICompanyService companyService, IMessenger messenger)
    {
        _navigationService = navigationService;
        _storesService = storesService;
        _companyService = companyService;
        _messenger = messenger;

        SelectedStore = new();
        EditableStore = new();

        InitializeStores();

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Company> != null)
                Companies = message.Data as ObservableCollection<Company>;
        });

        AddStore = new DelegateCommand(async () =>
        {
            try
            {
                var newStore = await _storesService.AddStore(EditableStore);
                Stores.Add(newStore);

                EditableStore = new Store();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        });

        RemoveStore = new DelegateCommand(async () =>
        {
            try
            {
                var response = await _storesService.DeleteStore(EditableStore);
                if(response)
                {
                    Stores.Remove(EditableStore);
                }

                EditableStore = new Store();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        });

        Back = new DelegateCommand(() =>
        {
            _navigationService.NavigateTo<WorkspaceViewModel>();
        });
    }

    private async void InitializeStores()
    {
        Stores = await _storesService.InitializeStoresAsync();
        Companies = await _companyService.InitializeCompanyAsync();
    }

    public DelegateCommand AddStore { get; private set; }
    public DelegateCommand RemoveStore { get; private set; }
    public DelegateCommand Back { get; private set; }
}