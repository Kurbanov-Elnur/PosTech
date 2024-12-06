using PosTech.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Services.Interfaces;

interface ICompanyService
{
    public Task<Company> AddCompany(Company newCompany);
    public Task<ObservableCollection<Company>> InitializeCompanyAsync();
}