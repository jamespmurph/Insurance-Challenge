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
            Console.WriteLine("  3. Exit");
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
                    Console.WriteLine("Goodbye.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
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

        Console.Write("Policy start date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var startDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }

        Console.Write("Policy end date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var endDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }

        Console.Write("Risk level (e.g. 1.5): ");
        if (!decimal.TryParse(Console.ReadLine(), out var riskLevel))
        {
            Console.WriteLine("Invalid risk level.");
            return;
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

            handler.CreatePolicy(policyHolder, startDate, endDate, vrn, riskLevel, isExistingCustomer);
            Console.WriteLine("Policy created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create policy: {ex.Message}");
        }
    }

    private static void CreateClaim(ClaimHandler handler)
    {
        // Follow the same pattern as CreatePolicy above.
        // Ask the user for: a policy ID, a date of incident, a description, and an amount.
        // Then call handler.CreateClaim(...) inside a try/catch, just like CreatePolicy does.
        throw new NotImplementedException();
    }
}

