using Domain.Common.Enums;
using Domain.ObjectValue;

namespace Domain.Common.Interfaces;

public interface IPerson
{
    public string FirstName { get; }
    public string MiddleName { get; }
    public string LastName { get; }
    public DateTime BirthDate { get; }
    public Gender Gender { get; }
    public string PhoneNumber { get; }
    public Address Address { get; }
}
