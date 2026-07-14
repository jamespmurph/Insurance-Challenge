using Rdt.Insurance.App.Models;
using Rdt.Insurance.App.Repositories;
//using System.Security.Claims;

namespace Rdt.Insurance.App.Services;

public class ClaimHandler
{
    private readonly IPolicyRepository _policyRepository;
    private readonly IClaimRepository _claimRepository;

    //Constructor to initialize the ClaimHandler with the required repositories.
    public ClaimHandler(IPolicyRepository policyRepository, IClaimRepository claimRepository)
    {
        _policyRepository = policyRepository;
        _claimRepository = claimRepository;
    }

    public void CreateClaim(Guid policyId, DateTime dateOfIncident, string description, decimal amountClaiming)
    {
        // Step 1: look up the policy using _policyRepository.
        //         Check IPolicyRepository.cs to see what methods are available.
        //         If the policy does not exist, throw an ArgumentException.

        // Step 2: validate the inputs against the business rules.
        //         Look at PolicyHandler.cs to see how validation is done there.

        // Step 3: create a new Claim object, populate its properties, and save it using _claimRepository.
        //         Check Models/Claim.cs to see all the properties you need to set.
        // sepretate these out with there own exception messaqges
        Policy policy = new();
        if (_policyRepository != null)
        {
            //add the typed model of policy
            policy = _policyRepository.GetById(policyId) ?? throw new ArgumentException($"Policy with ID {policyId} does not exist.");
        }

        if (dateOfIncident < policy.StartDate || dateOfIncident > policy.EndDate)
        {
            throw new ArgumentOutOfRangeException($"Date of incident {dateOfIncident} is outside the policy coverage period ({policy.StartDate} - {policy.EndDate}).");
        }

     if (description == string.Empty || description.IsNullOrWhiteSpace())
        {
            throw new ArgumentNullException("Description cannot be empty");
        }
     if (amountClaiming < 0)
        {
            throw new ArgumentOutOfRangeException("Amount claiming cannot be negative");
        }

        var dateFiled = DateTime.Now;       
        var isHighRisk = amountClaiming > 1000m;

        var claim = new Claim
        {
            Id = Guid.NewGuid(),

            PolicyId = policyId,
            Description = description,
            TotalClaimAmount = amountClaiming,
            DateOfIncident = dateOfIncident,
            DateFiled = dateFiled,
            IsHighRisk = isHighRisk

        };
        _claimRepository.Save(claim);
        policy.claims.Add(claim);
    }
}