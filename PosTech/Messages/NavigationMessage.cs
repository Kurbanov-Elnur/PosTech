using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostTech.Messages;

class NavigationMessage
{
    public BindableBase ViewModelType { get; set; }
}