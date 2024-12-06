using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Services.Interfaces;

interface IUserService
{
    Task<bool> Login(string Username, string Password);
    Task AddUser(string Username, string Password);
}