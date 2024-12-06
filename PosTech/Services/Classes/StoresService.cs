

using Microsoft.EntityFrameworkCore;
using PosTech.Data.Models;
using PosTech.Services.Interfaces;
using PosTech.Data.Contexts;
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
        ValidateCode(editableStore.Phone, "Telefon", 12, 12);
        ValidateCode(editableStore.Company.CompanyCode, "Firma Kodu", 1, 4);
        ValidateCode(editableStore.BranchCode, "Filial Kodu", 1, 4);
        ValidateCode(editableStore.TaxCode, "Vergi Kodu", 1, 20);

        var existingCompany = await _context.Companies
            .FirstOrDefaultAsync(c => c.CompanyCode == editableStore.Company.CompanyCode ||
                                       c.CompanyName == editableStore.Company.CompanyName);

        if (existingCompany == null)
        {
            existingCompany = new Company
            {
                CompanyCode = editableStore.Company.CompanyCode,
                CompanyName = editableStore.Company.CompanyName,
            };

            _context.Companies.Add(existingCompany);
            await _context.SaveChangesAsync();
        }

        var exists = await _context.Stores.AnyAsync(s =>
            s.CompanyId == existingCompany.Id &&
            (s.BranchCode == editableStore.BranchCode || s.BranchName == editableStore.BranchName));

        if (exists)
            throw new InvalidOperationException("Belə bir mağaza artıq mövcuddur.");

        var newStore = new Store
        {
            BranchCode = editableStore.BranchCode,
            BranchName = editableStore.BranchName,
            CityName = editableStore.CityName,
            Phone = editableStore.Phone,
            Status = editableStore.Status,
            Address = editableStore.Address,
            TaxCode = editableStore.TaxCode,
            Description = editableStore.Description,
            CompanyId = existingCompany.Id,
            Company = existingCompany,
        };

        try
        {
            _context.Stores.Add(newStore);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Xəta baş verdi mağazanı əlavə edərkən.", ex);
        }

        return newStore;
    }

    public async Task<bool> DeleteStore(Store editableStore)
    {
        try
        {
            var removedStore = _context.Stores.FirstOrDefault((s) => s.Id == editableStore.Id);

            _context.Stores.Remove(removedStore);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message); ;
        }
    }

    private void ValidateCode(string code, string fieldName, int minLength, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException($"{fieldName} boş ola bilməz.");

        if (!code.All(char.IsDigit))
            throw new ArgumentException($"{fieldName} yalnız rəqəmlərdən ibarət olmalıdır.");

        if (code.Length < minLength || code.Length > maxLength)
            throw new ArgumentException($"{fieldName} uzunluğu {minLength}-dən {maxLength}-ə qədər olmalıdır.");
    }

    public async Task<ObservableCollection<Store>> InitializeStoresAsync()
    {
        using (var context = new PostAppContext())
        {
            var storeList = await context.Stores
                .Include(s => s.Company)
                .ToListAsync();

            return new ObservableCollection<Store>(storeList);
        }
    }
}