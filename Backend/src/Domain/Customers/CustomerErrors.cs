using Domain.Common.Results;

namespace Domain.Customers;

public static class CustomerErrors
{
    public readonly static Error error;

    public static readonly Error FirstNameRequired;
    public static readonly Error LastNameRequired;
    public static readonly Error BirthDateInvalid;
    public static readonly Error GenderInvalid;
    public static readonly Error PhoneNumberRequired;
    public static readonly Error AddressInvalid;
}