using Microsoft.VisualBasic.Logging;
using PosTech.Data.Models;
using PosTech.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PosTech.ViewModels;

class WorkspaceViewModel : BindableBase
{
    private readonly INavigationService _navigationService;

    private DateTime _startDate = DateTime.Now.AddDays(-1);
    public DateTime startDate
    {
        get => _startDate;
        set => SetProperty(ref _startDate, value);
    }

    private DateTime _endDate = DateTime.Now;
    public DateTime endDate
    {
        get => _endDate;
        set => SetProperty(ref _endDate, value);
    }

    public ObservableCollection<Receipt> ReceiptDatas { get; set; }

    public WorkspaceViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        ReceiptDatas = new();

        ReceiptDatas.Add(new Receipt
        {
            CashierName = "John Doe",
            Type = "Satis",
            DocumentNo = "299518",
            Date = DateTime.Now,
            Amount = 100.00m,
            EDV = 18.00m,
            EDVExcluded = 82.00m,
            Discount = 5.00m,
            Paid = 95.00m,
            ZNo = "Z1",
            TokenZ = "Token1",
            Delivery = "Delivered",
            DeliveryDate = DateTime.Now
        }); ReceiptDatas.Add(new Receipt
        {
            CashierName = "John Doe",
            Type = "Satis",
            DocumentNo = "299518",
            Date = DateTime.Now,
            Amount = 100.00m,
            EDV = 18.00m,
            EDVExcluded = 82.00m,
            Discount = 5.00m,
            Paid = 95.00m,
            ZNo = "Z1",
            TokenZ = "Token1",
            Delivery = "Delivered",
            DeliveryDate = DateTime.Now
        }); ReceiptDatas.Add(new Receipt
        {
            CashierName = "John Doe",
            Type = "Satis",
            DocumentNo = "299518",
            Date = DateTime.Now,
            Amount = 100.00m,
            EDV = 18.00m,
            EDVExcluded = 82.00m,
            Discount = 5.00m,
            Paid = 95.00m,
            ZNo = "Z1",
            TokenZ = "Token1",
            Delivery = "Delivered",
            DeliveryDate = DateTime.Now
        });

        OpenStoreView = new DelegateCommand(() =>
        {
            _navigationService.NavigateTo<StoreViewModel>();
        });
        
        OpenCompanyView = new DelegateCommand(() =>
        {
            _navigationService.NavigateTo<CompanyViewModel>();
        });
    }

    public DelegateCommand OpenStoreView { get; private set; }
    public DelegateCommand OpenCompanyView { get; private set; }
}