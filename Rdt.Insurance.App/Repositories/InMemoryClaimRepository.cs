using Rdt.Insurance.App.Models;

namespace Rdt.Insurance.App.Repositories;

public class InMemoryClaimRepository : IClaimRepository
{
    private readonly List<Claim> _claims = new();

    public void Save(Claim claim)
    {
        // Look at InMemoryPolicyRepository.cs for an example of how this is done for policies.
       _claims.Add(claim);
    }

    public IReadOnlyList<Claim> GetAll() => _claims;

    public IReadOnlyList<Claim> GetClaimsByPolicyId(Guid policyId) =>
        _claims.Where(c => c.PolicyId == policyId).ToList();
}
