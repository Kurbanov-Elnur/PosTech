using Microsoft.EntityFrameworkCore;
using PosTech.Data.Contexts;
using PosTech.Data.Models;
using PosTech.Services.Interfaces;
using System.Collections.ObjectModel;

namespace PosTech.Services.Classes;

class CompanyService : ICompanyService
{
    private readonly PostAppContext _context;

    public CompanyService(PostAppContext context)
    {
        _context = context;
    }

    public async Task<Company> AddCompany(Company editableCompany)
    {
        ValidateCode(editableCompany.CompanyCode, "Sirkət Kodu", 1, 4);

        var existingCompany = await _context.Companies
            .FirstOrDefaultAsync(c => c.CompanyCode == editableCompany.CompanyCode ||
                                 c.CompanyName == editableCompany.CompanyName);

        if (existingCompany == null)
        {
            existingCompany = new Company
            {
                CompanyCode = editableCompany.CompanyCode,
                CompanyName = editableCompany.CompanyName,
            };

            _context.Companies.Add(existingCompany);
            await _context.SaveChangesAsync();

            return existingCompany;
        }
        else
            throw new InvalidOperationException("Belə bir şirkət artıq mövcuddur.");
    }

    public async Task<ObservableCollection<Company>> InitializeCompanyAsync()
    {
        using (var context = new PostAppContext())
        {
            var companyList = await context.Companies.ToListAsync();

            return new ObservableCollection<Company>(companyList);
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
}