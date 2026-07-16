using Rdt.Insurance.App.Models;

namespace Rdt.Insurance.App.Repositories;

public class InMemoryPolicyRepository : IPolicyRepository
{
    private readonly List<Policy> _policies = new();

    public void Save(Policy policy) => _policies.Add(policy);

    public Policy? GetById(Guid id) =>
        _policies.FirstOrDefault(p => p.Id == id);

    public Policy? GetPolicyByPolicyHolderName(string name) =>
        _policies.FirstOrDefault(p =>
            $"{p.PolicyHolder.FirstName} {p.PolicyHolder.LastName}"
                .Equals(name, StringComparison.OrdinalIgnoreCase));
    public IReadOnlyList<Policy> GetAll() => _policies;
}
