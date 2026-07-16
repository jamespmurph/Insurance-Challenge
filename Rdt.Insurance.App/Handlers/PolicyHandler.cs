using Rdt.Insurance.App.Models;
using Rdt.Insurance.App.Repositories;

namespace Rdt.Insurance.App.Services;

public class PolicyHandler
{
    private readonly IPolicyRepository _repository;

    public PolicyHandler(IPolicyRepository repository)
    {
        _repository = repository;
    }

    public Guid CreatePolicy(PolicyHolder policyHolder, DateTime startDate, DateTime endDate, string vehicleRegistrationNumber, decimal riskLevel, bool existingCustomer)
    {
        if (policyHolder == null)
            throw new ArgumentException("Policy holder is required.");

        if (endDate <= startDate)
            throw new ArgumentException("End date must be after start date.");

        if (string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            throw new ArgumentException("Vehicle registration number is required.");

        if (riskLevel <= 0.0m || riskLevel >= 5.0m)
            throw new ArgumentOutOfRangeException("Risk level must be between 0.0 and 5.0.");

        var today = DateTime.Today;
        var age = today.Year - policyHolder.DateOfBirth.Year;
        if (policyHolder.DateOfBirth.Date > today.AddYears(-age)) age--;

        //Young driver surcharge applied to drivers under 25
        var youngDriverSurcharge = (policyHolder.DateOfBirth == default || age < 25) ? 500m : 0m;

        //Ceiling price for a policy
        var maximumPremium = existingCustomer ? 2000m : 2200m;

        //Rate charged per unit of risk
        var currentSchemeRate = 400;

        //Fixed floor charge
        var basePremium = 100;
        var windscreenCover = 250;
        var premium = (riskLevel * currentSchemeRate) + basePremium + windscreenCover + youngDriverSurcharge;

        var policy = new Policy
        {
            Id = Guid.NewGuid(),
            PolicyHolder = policyHolder,
            StartDate = startDate,
            EndDate = endDate,
            Premium = premium,
            VehicleRegistrationNumber = vehicleRegistrationNumber
        };

        if (policy.Premium >= maximumPremium)
            throw new InvalidOperationException($"Calculated premium of {policy.Premium} exceeds the maximum allowed premium of {maximumPremium}.");

        _repository.Save(policy);
        return policy.Id;
    }
}
