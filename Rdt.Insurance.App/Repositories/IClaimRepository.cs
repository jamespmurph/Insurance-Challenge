using Rdt.Insurance.App.Models;

namespace Rdt.Insurance.App.Repositories;

public interface IClaimRepository
{
    void Save(Claim claim);
 IReadOnlyList<Claim> GetAll();

    IReadOnlyList<Claim> GetClaimsByPolicyId(Guid policy);
}
