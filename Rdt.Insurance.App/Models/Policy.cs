namespace Rdt.Insurance.App.Models;

public class Policy
{
    public Guid Id { get; set; }

    public PolicyHolder PolicyHolder { get; set; } = default!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Premium { get; set; }

    public string VehicleRegistrationNumber { get; set; } = string.Empty;

    public List<Claim> claims { get; set; } = new();
}
