﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Messages;

class NavigationMessage
{
    public BindableBase ViewModelType { get; set; }
}