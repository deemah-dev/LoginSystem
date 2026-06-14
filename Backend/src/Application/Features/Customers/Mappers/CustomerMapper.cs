using Application.Features.Customers.Dtos;
using Domain.Customers;

namespace Application.Features.Customers.Mappers;

public static class CustomerMapper
{
    public static CustomerDto ToDto(this Customer customer) =>
    new()
    {
        Id = customer.Id,
        FullName = customer.MiddleName is null
        ? $"{customer.FirstName} {customer.LastName}"
        : $"{customer.FirstName} {customer.MiddleName} {customer.LastName}",
        BirthDate = customer.BirthDate,
        PhoneNumber = customer.PhoneNumber ?? string.Empty,
        Gender = customer.Gender,
        Address = new()
        {
            Country = customer.Address.Country,
            City = customer.Address.City,
            Street = customer.Address.Street,
        }
    };

    public static List<CustomerDto> ToDtos(this IEnumerable<Customer> customers) =>
    [.. customers.Select(c => c.ToDto())];
}