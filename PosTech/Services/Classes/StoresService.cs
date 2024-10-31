

using Microsoft.EntityFrameworkCore;
using PosTech.Data.Models;
using PosTech.Services.Interfaces;
using PostTech.Data.Contexts;
using PostTech.Data.Models;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace PosTech.Services.Classes;

class StoresService : IStoresService
{
    private readonly PostAppContext _context;

    public StoresService(PostAppContext context)
    {
        _context = context;
    }

    public async Task<Store> AddStore(Store editableStore)
    {
        Store newStore;
        var digitsOnlyRegex = new Regex(@"^\d+$");

        if (!digitsOnlyRegex.IsMatch(editableStore.Phone) || editableStore.Phone.Length != 12)
            throw new ArgumentException("Telefon yalnız rəqəmlərdən ibarət olmalı və 12 simvol uzunluğunda olmalıdır.");

        if (!digitsOnlyRegex.IsMatch(editableStore.Company.CompanyCode) ||
            editableStore.Company.CompanyCode.Length == 0 ||
            editableStore.Company.CompanyCode.Length > 4)
            throw new ArgumentException("Firma Kodu yalnız rəqəmlərdən ibarət olmalıdır və uzunluğu 1-dən 4-ə qədər olmalıdır.");

        if (!digitsOnlyRegex.IsMatch(editableStore.BranchCode) ||
            editableStore.BranchCode.Length == 0 ||
            editableStore.BranchCode.Length > 4)
            throw new ArgumentException("Filial Kodu yalnız rəqəmlərdən ibarət olmalıdır və uzunluğu 1-dən 4-ə qədər olmalıdır.");

        if (!digitsOnlyRegex.IsMatch(editableStore.Company.TaxCode) ||
            editableStore.Company.TaxCode.Length == 0 ||
            editableStore.Company.TaxCode.Length > 20)
            throw new ArgumentException("Vergi Kodu yalnız rəqəmlərdən ibarət olmalıdır və uzunluğu 1-dən 20-ə qədər olmalıdır.");

        var existingCompany = await _context.Companies
            .FirstOrDefaultAsync(c => c.CompanyCode == editableStore.Company.CompanyCode ||
                                       c.CompanyName == editableStore.Company.CompanyName ||
                                       c.TaxCode == editableStore.Company.TaxCode);

        if (existingCompany == null)
        {
            existingCompany = new Company
            {
                CompanyCode = editableStore.Company.CompanyCode,
                CompanyName = editableStore.Company.CompanyName,
                TaxCode = editableStore.Company.CompanyCode,
            };

            _context.Companies.Add(existingCompany);
            await _context.SaveChangesAsync();
        }

        var exists = await _context.Stores.AnyAsync(s =>
            s.CompanyId == existingCompany.Id &&
            (s.BranchCode == editableStore.BranchCode ||
             s.BranchName == editableStore.BranchName));

        if (exists)
            throw new InvalidOperationException("Belə bir mağaza artıq mövcuddur.");

        try
        {
            newStore = new Store()
            {
                BranchCode = editableStore.BranchCode,
                BranchName = editableStore.BranchName,
                CityName = editableStore.CityName,
                Phone = editableStore.Phone,
                Status = editableStore.Status,
                Address = editableStore.Address,
                Description = editableStore.Description,
                CompanyId = existingCompany.Id,
                Company = existingCompany,
            };

            _context.Stores.Add(newStore);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw;
        }

        return newStore;
    }

    public async Task<ObservableCollection<Store>> InitializeStoresAsync()
    {
        var storeList = await _context.Stores
            .Include(s => s.Company)
            .ToListAsync();

        var observableStores = new ObservableCollection<Store>(storeList);
        return observableStores;
    }
}