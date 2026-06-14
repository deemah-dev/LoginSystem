using Application.Features.Customers.Commands.CreateCustomer;
using Domain.Common.Enums;

namespace Tests.Common.Customers;

public static class CustomerCommandFactory
{
    public static CreateCustomerCommand Create_CreateCustomerCommand
    (string? firstName = "John",
        string? middleName = "M",
        string? lastName = "Doe",
        DateTime? birthDate = null,
        Gender? gender = null,
        string? phoneNumber = "555-1234",
        string? country = "USA",
        string? city = "Anytown",
        string? street = "123 Main St")
    {
        return new CreateCustomerCommand(
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