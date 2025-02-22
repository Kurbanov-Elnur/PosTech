﻿using PosTech.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Services.Interfaces;

interface IStoresService
{
    public Task<Store> AddStore(Store newStore);
    public Task<bool> DeleteStore(Store store);
    public Task<ObservableCollection<Store>> InitializeStoresAsync();
}