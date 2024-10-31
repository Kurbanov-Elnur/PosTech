using PosTech.Data.Models;

namespace PostTech.Data.Models;

public class Store
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BranchCode { get; set; }
    public string BranchName { get; set; }
    public string CityName { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }

    public string CompanyId { get; set; }
    public Company Company { get; set; } = new();
}