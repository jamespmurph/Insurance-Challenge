using Rdt.Insurance.App.Models;
using Rdt.Insurance.App.Repositories;
using Rdt.Insurance.App.Services;

namespace Rdt.Insurance.App;

internal class Program
{
    static void Main(string[] args)
    {
        var policyRepository = new InMemoryPolicyRepository();
        var claimRepository = new InMemoryClaimRepository();

        var policyHandler = new PolicyHandler(policyRepository);
        var claimHandler = new ClaimHandler(policyRepository, claimRepository);

        while (true)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("  1. Create Policy");
            Console.WriteLine("  2. Create Claim");
            Console.WriteLine("  3. View all Policies");
            Console.WriteLine("  4. View all Claims");
            Console.WriteLine("  5. View Claims by Policy ID");
            Console.WriteLine("  6. Exit");
            Console.Write("> ");

            var choice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreatePolicy(policyHandler);
                    break;
                case "2":
                    CreateClaim(claimHandler);
                    break;
                case "3":
                    ViewAllPolicies(policyRepository);
                    break;
                case "4":
                    ViewAllClaims(claimRepository);
                    break;
                case "5":
                    ViewClaimsByPolicyId(claimRepository);
                    break;
                case "6":
                    Console.WriteLine("Exiting the application.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please enter 1-6.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void CreatePolicy(PolicyHandler handler)
    {
        Console.WriteLine("Please enter policy details:");

        Console.Write("First name: ");
        var firstName = Console.ReadLine() ?? string.Empty;

        Console.Write("Last name: ");
        var lastName = Console.ReadLine() ?? string.Empty;

        Console.Write("Email address: ");
        var email = Console.ReadLine() ?? string.Empty;

        Console.Write("Vehicle registration number: ");
        var vrn = Console.ReadLine() ?? string.Empty;

        DateTime startDate;
        Console.Write("Policy start date (yyyy-MM-dd): ");
        while (!DateTime.TryParse(Console.ReadLine(), out startDate))
        {
            Console.WriteLine("Invalid date format.");
            Console.Write("Please enter the policy start date (yyyy-MM-dd): ");
        }

        DateTime endDate;
        Console.Write("Policy end date (yyyy-MM-dd): ");
        while (!DateTime.TryParse(Console.ReadLine(), out endDate))
        {
            Console.WriteLine("Invalid date format.");
            Console.Write("Please enter the policy end date (yyyy-MM-dd): ");
        }

        decimal riskLevel;
        Console.Write("Risk level (e.g. 1.5): ");
        while (!decimal.TryParse(Console.ReadLine(), out riskLevel))
        {
            Console.WriteLine("Invalid risk level.");
            Console.Write("Please enter the risk level (e.g. 1.5): ");
        }

        Console.Write("Existing customer? (y/n): ");
        var isExistingCustomer = Console.ReadLine()?.Trim().Equals("y", StringComparison.OrdinalIgnoreCase) ?? false;

        try
        {
            var policyHolder = new PolicyHolder
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email
            };

            var policyId = handler.CreatePolicy(policyHolder, startDate, endDate, vrn, riskLevel, isExistingCustomer);
            Console.WriteLine($"Policy created successfully. Policy ID: {policyId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create policy: {ex.Message}");
        }
    }

    private static void CreateClaim(ClaimHandler handler)
    {
        Console.WriteLine("Please enter claim details");

        Guid policyId;
        Console.Write("Policy ID: ");
        while (!Guid.TryParse(Console.ReadLine(), out policyId))
        {
            Console.WriteLine("Invalid policy ID format.");
            Console.Write("Please enter the policy ID: ");
        }

        DateTime dateOfIncident;
        Console.Write("Date of incident (yyyy-MM-dd): ");
        while (!DateTime.TryParse(Console.ReadLine(), out dateOfIncident))
        {
            Console.WriteLine("Invalid date format.");
            Console.Write("Please enter the date of incident (yyyy-MM-dd): ");
        }

        Console.Write("Description: ");
        var description = Console.ReadLine() ?? string.Empty;

        decimal amountClaiming;
        Console.Write("Amount claiming: ");
        while (!decimal.TryParse(Console.ReadLine(), out amountClaiming))
        {
            Console.WriteLine("Invalid amount format.");
            Console.Write("Amount claiming: ");
        }

        try
        {
            handler.CreateClaim(policyId, dateOfIncident, description, amountClaiming);
            Console.WriteLine("Claim created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create claim: {ex.Message}");
        }
    }

    private static void ViewAllPolicies(IPolicyRepository repository)
    {
        var policies = repository.GetAll();

        if (policies.Count == 0)
        {
            Console.WriteLine("No policies found.");
            return;
        }

        foreach (var policy in policies)
        {
            Console.WriteLine($"ID: {policy.Id}, Holder: {policy.PolicyHolder.FirstName} {policy.PolicyHolder.LastName}, Premium: {policy.Premium:C}, Start: {policy.StartDate:yyyy-MM-dd}, End: {policy.EndDate:yyyy-MM-dd}");
        }
    }

    private static void ViewAllClaims(IClaimRepository repository)
    {
        var claims = repository.GetAll();

        if (claims.Count == 0)
        {
            Console.WriteLine("No claims found.");
            return;
        }

        foreach (var claim in claims)
        {
            Console.WriteLine($"ID: {claim.Id}, Policy ID: {claim.PolicyId}, Date of Incident: {claim.DateOfIncident:yyyy-MM-dd}, Amount Claiming: {claim.TotalClaimAmount:C}, Description: {claim.Description}");
        }
    }

    private static void ViewClaimsByPolicyId(IClaimRepository repository)
    {
        Guid policyId;
        Console.Write("Enter Policy ID: ");
        while (!Guid.TryParse(Console.ReadLine(), out policyId))
        {
            Console.WriteLine("Invalid Policy ID format.");
            Console.Write("Enter Policy ID: ");
        }

        var claims = repository.GetClaimsByPolicyId(policyId);

        if (claims.Count == 0)
        {
            Console.WriteLine($"No claims found for Policy ID: {policyId}");
            return;
        }

        foreach (var claim in claims)
        {
            Console.WriteLine($"ID: {claim.Id}, Date of Incident: {claim.DateOfIncident:yyyy-MM-dd}, Amount Claiming: {claim.TotalClaimAmount:C}, Description: {claim.Description}");
        }
    }
}




























