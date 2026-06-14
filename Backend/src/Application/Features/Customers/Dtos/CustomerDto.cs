using Domain.Common.Enums;
using Domain.ObjectValue;

namespace Application.Features.Customers.Dtos;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
    public Gender Gender { get; set; }
    public string? UserId { get; set; }
}