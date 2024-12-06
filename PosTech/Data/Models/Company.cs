namespace PosTech.Data.Models;

public class Company
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }

    public ICollection<Store> Stores { get; set; }
}