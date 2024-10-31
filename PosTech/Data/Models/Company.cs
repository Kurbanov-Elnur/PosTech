using PostTech.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Data.Models;

public class Company
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string TaxCode { get; set; }

    public ICollection<Store> Stores { get; set; }
}