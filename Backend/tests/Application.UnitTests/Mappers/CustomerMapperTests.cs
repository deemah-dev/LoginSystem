using Application.Features.Customers.Mappers;
using Tests.Common.Customers;
using Xunit;

namespace Application.UnitTests.Mappers;

public class CustomerMapperTests
{
    [Fact]
    void ToDto_ShouldMapCorrectly()
    {
        var customer = CustomerFactory.CreateCustomer().Value;

        var dto = customer.ToDto();

        Assert.NotNull(dto);
        Assert.Equal(customer.Address.Country, dto.Address.Country);
        Assert.Equal(customer.Address.City, dto.Address.City);
        Assert.Equal(customer.Address.Street, dto.Address.Street);
        Assert.Equal(customer.BirthDate, dto.BirthDate);
        Assert.Equal(customer.Gender, dto.Gender);
        Assert.Equal(customer.Id, dto.Id);
        Assert.Equal(customer.PhoneNumber, dto.PhoneNumber);
        Assert.Equal(customer.UserId, dto.UserId);
        if (customer.MiddleName is null)
        {
            Assert.Equal($"{customer.FirstName} {customer.LastName}", dto.FullName);
        }
        else
        {
            Assert.Equal($"{customer.FirstName} {customer.MiddleName} {customer.LastName}", dto.FullName);
        }
    }
}