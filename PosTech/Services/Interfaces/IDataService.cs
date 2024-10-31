using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostTech.Services.Interfaces;

interface IDataService
{
    public void SendData(object data);
}