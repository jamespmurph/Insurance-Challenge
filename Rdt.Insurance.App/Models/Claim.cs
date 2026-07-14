namespace Rdt.Insurance.App.Models;

public class Claim
{
    public Guid Id { get; set; }

    public Guid PolicyId { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal TotalClaimAmount { get; set; }

    public DateTime DateOfIncident { get; set; }

    public DateTime DateFiled { get; set; }

    public bool IsHighRisk { get; set; }
}
