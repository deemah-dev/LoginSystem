using Domain.Common;
using Domain.Common.Enums;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.ObjectValue;


namespace Domain.Customers;

public class Customer : AuditableEntity, IPerson
{
    private Customer(string firstName,
                        string? middleName,
                        string lastName,
                        DateTime birthDate,
                        Gender gender,
                        string phoneNumber,
                        string country,
                        string city,
                        string street) : base()
    {
        FirstName = firstName;
        MiddleName = middleName ?? "";
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
        PhoneNumber = phoneNumber;
        Address = new()
        {
            Country = country,
            City = city,
            Street = street
        };
        IsDeleted = false;
    }

    public string? UserId { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public string PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool IsDeleted { get; private set; }

    public static Result<Customer> Create(string firstName,
                        string? middleName,
                        string lastName,
                        DateTime birthDate,
                        Gender gender,
                        string phoneNumber,
                        string country,
                        string city,
                        string street)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return CustomerErrors.FirstNameRequired;
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return CustomerErrors.LastNameRequired;
        }
        if (birthDate > DateTime.UtcNow.AddYears(-18))
        {
            return CustomerErrors.BirthDateInvalid;
        }
        if (!Enum.IsDefined(gender))
        {
            return CustomerErrors.GenderInvalid;
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return CustomerErrors.PhoneNumberRequired;
        }

        if (string.IsNullOrWhiteSpace(country)
        || string.IsNullOrWhiteSpace(city)
        || string.IsNullOrWhiteSpace(street))
        {
            return CustomerErrors.AddressInvalid;
        }

        return new Customer(firstName,
                            middleName,
                            lastName,
                            birthDate,
                            gender,
                            phoneNumber,
                            country,
                            city,
                            street);
    }

    public void Delete()
    {
        UserId = "Deleted";
        FirstName = "Deleted";
        MiddleName = "Deleted";
        LastName = "Deleted";
        BirthDate = DateTime.MinValue;
        PhoneNumber = "Deleted";
        Address = new()
        {
            Country = "",
            City = "",
            Street = ""
        };
        IsDeleted = false;
    }

    public void LinkToAccount(string userId)
    {
        UserId = userId;
    }

    public void UpdatePhone(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
}