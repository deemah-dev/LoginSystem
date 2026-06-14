using Domain.Common.Enums;
using Domain.Customers;
using Tests.Common.Customers;
using Xunit;

namespace Domain.UnitTests.Customers;

public class CustomerTests
{
    [Fact]
    public void CreateCustomer_ShouldSucceed_WithValidData()
    {
        var result = CustomerFactory.CreateCustomer();

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);

        var customer = result.Value;

        Assert.NotNull(customer);
        Assert.Equal("John", customer.FirstName);
        Assert.Equal("Doe", customer.LastName);
        Assert.Equal("555-1234", customer.PhoneNumber);
    }

    [Fact]
    public void CreateCustomer_ShouldFail_WhenFirstNameMissing()
    {
        var result = CustomerFactory.CreateCustomer(firstName: string.Empty);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(CustomerErrors.FirstNameRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void CreateCustomer_ShouldFail_WhenLastNameMissing()
    {
        var result = CustomerFactory.CreateCustomer(lastName: string.Empty);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(CustomerErrors.LastNameRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void CreateCustomer_ShouldFail_WhenUnderage()
    {
        var result = CustomerFactory.CreateCustomer(birthDate: DateTime.UtcNow.AddYears(-17));

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(CustomerErrors.BirthDateInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void CreateCustomer_ShouldFail_WhenGenderInvalid()
    {
        var result = CustomerFactory.CreateCustomer(gender: (Gender)int.MaxValue);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(CustomerErrors.GenderInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void CreateCustomer_ShouldFail_WhenPhoneNumberMissing()
    {
        var result = CustomerFactory.CreateCustomer(phoneNumber: string.Empty);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(CustomerErrors.PhoneNumberRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void CreateCustomer_ShouldFail_WhenAddressInvalid()
    {
        var result = CustomerFactory.CreateCustomer(country: string.Empty, city: string.Empty, street: string.Empty);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(CustomerErrors.AddressInvalid.Code, result.TopError.Code);
    }
}
