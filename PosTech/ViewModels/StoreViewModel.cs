using Microsoft.VisualBasic.Logging;
using PosTech.Data.Models;
using PosTech.Services.Interfaces;
using PostTech.Data.Models;
using PostTech.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PostTech.ViewModels;

class StoreViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IStoresService _storesService;

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
                EditableStore = new Store
                {
                    BranchCode = _selectedStore.BranchCode,
                    BranchName = _selectedStore.BranchName,
                    CityName = _selectedStore.CityName,
                    Phone = _selectedStore.Phone,
                    Status = _selectedStore.Status,
                    Address = _selectedStore.Address,
                    Description = _selectedStore.Description,
                    Company = new Company()
                    {
                        CompanyCode = _selectedStore.Company.CompanyCode,
                        CompanyName = _selectedStore.Company.CompanyName,
                        TaxCode = _selectedStore.Company.TaxCode
                    },
                    CompanyId = _selectedStore.CompanyId
                };
            }
        }
    }

    public ObservableCollection<Store> Stores { get; set; } = new();

    public StoreViewModel(INavigationService navigationService, IStoresService storesService)
    {
        _navigationService = navigationService;
        _storesService = storesService;
        SelectedStore = new();
        EditableStore = new();

        InitializeStores();

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

        RemoveStore = new DelegateCommand(() =>
        {
        });

        Back = new DelegateCommand(() =>
        {
            _navigationService.NavigateTo<WorkspaceViewModel>();
        });
    }

    private async void InitializeStores()
    {
        Stores = await _storesService.InitializeStoresAsync();
    }

    public DelegateCommand AddStore { get; private set; }
    public DelegateCommand RemoveStore { get; private set; }
    public DelegateCommand Back { get; private set; }
}