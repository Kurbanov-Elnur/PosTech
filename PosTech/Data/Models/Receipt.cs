using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostTech.Data.Models;

class Receipt
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CashierName { get; set; }
    public string Type { get; set; }
    public string DocumentNo { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public decimal EDV { get; set; }
    public decimal EDVExcluded { get; set; }
    public decimal Discount { get; set; }
    public decimal Paid { get; set; }
    public string ZNo { get; set; }
    public string TokenZ { get; set; }
    public string Delivery { get; set; }
    public DateTime DeliveryDate { get; set; }
}