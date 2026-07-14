using Rdt.Insurance.App.Models;

namespace Rdt.Insurance.App.Repositories;

public interface IPolicyRepository
{
    void Save(Policy policy);

    Policy? GetById(Guid id);

    Policy? GetPolicyByPolicyHolderName(string name);
}
