using Microsoft.EntityFrameworkCore;
using PosTech.Data.Contexts;
using PosTech.Data.Models;
using PosTech.Services.Interfaces;
using System.Text.RegularExpressions;
using System.Windows;

namespace PosTech.Services.Classes;

class UserService : IUserService
{
    private readonly PostAppContext _context;
    private readonly IDataService _dataService;
    private User? _currentUser;

    public UserService(PostAppContext context, IDataService dataService)
    {
        _context = context;
        _dataService = dataService;
    }

    public async Task<bool> Login(string Username, string Password)
    {
        if (!Regex.IsMatch(Username, @"^(?=.*[A-Za-z])[A-Za-z0-9.]{3,50}$"))
            throw new ArgumentException("Yanlış istifadəçi adı. O, 3-dən 50 simvola qədər olmalı, yalnız hərflər, rəqəmlər və nöqtələrdən ibarət olmalı, ən azı bir hərf olmalı və boşluq olmamalıdır.");

        if (!Regex.IsMatch(Password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[\W_])[^\s]{8,26}$"))
            throw new ArgumentException("Yanlış parol. O, 8-dən 26 simvola qədər olmalı, hərflər, rəqəmlər, xüsusi simvolları daxil etməli və boşluq olmamalıdır.");

        _currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);

        if (_currentUser != null && BCrypt.Net.BCrypt.Verify(Password, _currentUser.Password))
            return true;
        else
            return false;
    }

    public async Task AddUser(string Username, string Password)
    {
        if (!Regex.IsMatch(Username, @"^(?=.*[A-Za-z])[A-Za-z0-9.]{3,50}$"))
            throw new ArgumentException("Yanlış istifadəçi adı. O, 3-dən 50 simvola qədər olmalı, yalnız hərflər, rəqəmlər və nöqtələrdən ibarət olmalı, ən azı bir hərf olmalı və boşluq olmamalıdır.");

        if (!Regex.IsMatch(Password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[\W_])[^\s]{8,26}$"))
            throw new ArgumentException("Yanlış parol. O, 8-dən 26 simvola qədər olmalı, hərflər, rəqəmlər, xüsusi simvolları daxil etməli və boşluq olmamalıdır.");

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

        var newUser = new User
        {
            Name = "Elnur",
            Surname = "Mamedov",
            Username = Username,
            Password = hashedPassword,
            UserRole = Role.Admin,
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
    }
}