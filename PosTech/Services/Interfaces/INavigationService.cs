using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Services.Interfaces;

public interface INavigationService
{
    public void NavigateTo<T>() where T : BindableBase;
}