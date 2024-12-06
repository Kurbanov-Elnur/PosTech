using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PosTech.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PosTech.Data.Contexts;

class PostAppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Receipt> ReceiptDatas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(builder.GetConnectionString("Default"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();
        var stores = modelBuilder.Entity<Store>();
        var companies = modelBuilder.Entity<Company>();
        var receiptDatas = modelBuilder.Entity<Receipt>();

        //User DB 
        user.HasKey(u => u.Id);

        user.Property(u => u.Name)
                .IsRequired();

        user.Property(u => u.Surname)
                .IsRequired();

        user.Property(u => u.Username)
                .IsRequired();

        user.HasIndex(u => u.Username)
            .IsUnique();

        user.Property(u => u.Password)
                .IsRequired();

        user.Property(u => u.UserRole)
            .HasConversion<string>()
            .IsRequired();

        companies.HasKey(c => c.Id);

        companies.Property(c => c.CompanyCode)
                .IsRequired();
        companies.HasIndex(c => c.CompanyCode).IsUnique();

        companies.Property(c => c.CompanyName)
               .IsRequired();

        companies.HasIndex(c => c.CompanyName).IsUnique();

        //Store Data DB
        stores.HasKey(s => s.Id);

        stores.Property(s => s.BranchCode)
                .IsRequired();

        stores.Property(s => s.BranchName)
                .IsRequired();

        stores.Property(s => s.CityName)
            .IsRequired();

        stores.Property(s => s.Phone)
            .IsRequired();

        stores.Property(s => s.Status)
            .IsRequired();

        stores.Property(s => s.Address)
            .IsRequired();

        stores.Property(s => s.TaxCode)
            .IsRequired();

        stores.HasIndex(s => s.TaxCode)
            .IsUnique();

        stores.Property(s => s.Description);

        stores.HasOne(s => s.Company)
            .WithMany(c => c.Stores)
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Receipt Data DB
        receiptDatas.HasKey(r => r.Id);

        receiptDatas.Property(r => r.CashierName)
            .IsRequired();

        receiptDatas.Property(r => r.Type)
            .IsRequired();

        receiptDatas.Property(r => r.DocumentNo)
            .IsRequired();

        receiptDatas.HasIndex(r => r.DocumentNo)
            .IsUnique();

        receiptDatas.Property(r => r.Date)
            .IsRequired();

        receiptDatas.Property(r => r.Amount)
            .IsRequired();

        receiptDatas.Property(r => r.EDV)
            .IsRequired();

        receiptDatas.Property(r => r.EDVExcluded)
            .IsRequired();

        receiptDatas.Property(r => r.Discount)
            .IsRequired();

        receiptDatas.Property(r => r.Paid)
            .IsRequired();

        receiptDatas.Property(r => r.ZNo)
            .IsRequired();

        receiptDatas.Property(r => r.TokenZ);

        receiptDatas.Property(r => r.Delivery)
            .IsRequired();

        receiptDatas.Property(r => r.DeliveryDate)
            .IsRequired();
    }
}