using Domain.Common.Enums;
using Domain.Common.Results;
using Domain.Customers;

namespace Tests.Common.Customers;

public static class CustomerFactory
{
    public static Result<Customer> CreateCustomer(
        string? firstName = "John",
        string? middleName = "M",
        string? lastName = "Doe",
        DateTime? birthDate = null,
        Gender? gender = null,
        string? phoneNumber = "555-1234",
        string? country = "USA",
        string? city = "Anytown",
        string? street = "123 Main St")
    {
        return Customer.Create(
            firstName ?? string.Empty,
            middleName,
            lastName ?? string.Empty,
            birthDate ?? DateTime.UtcNow.AddYears(-18),
            gender ?? Gender.Male,
            phoneNumber ?? string.Empty,
            country ?? string.Empty,
            city ?? string.Empty,
            street ?? string.Empty);
    }
}
