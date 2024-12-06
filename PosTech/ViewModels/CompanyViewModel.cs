using PosTech.Data.Models;
using PosTech.Services.Classes;
using PosTech.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.ViewModels;

class CompanyViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly ICompanyService _companiesService;
    private readonly IDataService _dataService;


    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    private Company _editableCompany;
    public Company EditableCompany
    {
        get => _editableCompany;
        set => SetProperty(ref _editableCompany, value);
    }

    private Company _selectedCompany;
    public Company SelectedCompany
    {
        get => _selectedCompany;
        set
        {
            SetProperty(ref _selectedCompany, value);
            if (_selectedCompany != null)
            {
                EditableCompany = new Company
                {
                    CompanyCode = _selectedCompany.CompanyCode,
                    CompanyName = _selectedCompany.CompanyName
                };
            }
        }
    }

    public ObservableCollection<Company> Companies { get; set; } = new();

    public CompanyViewModel(INavigationService navigationService, ICompanyService companiesService, IDataService dataService)
    {
        _navigationService = navigationService;
        _companiesService = companiesService;
        _dataService = dataService;

        SelectedCompany = new();
        EditableCompany = new();

        InitializeCompanies();

        AddCompany = new DelegateCommand(async () =>
        {
            try
            {
                var newCompany = await _companiesService.AddCompany(EditableCompany);
                Companies.Add(newCompany);
                _dataService.SendData(Companies);

                EditableCompany = new Company();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        });

        RemoveCompany = new DelegateCommand(() =>
        {
        });

        Back = new DelegateCommand(() =>
        {
            _navigationService.NavigateTo<WorkspaceViewModel>();
        });
    }

    public DelegateCommand AddCompany { get; private set; }
    public DelegateCommand RemoveCompany { get; private set; }
    public DelegateCommand Back { get; private set; }

    private async void InitializeCompanies()
    {
        Companies = await _companiesService.InitializeCompanyAsync();
    }
}