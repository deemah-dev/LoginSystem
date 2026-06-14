using Domain.Common;
using Domain.Common.Enums;
using Domain.Common.Results;
using Domain.Employees.Enums;

namespace Domain.Employees;

public class Employee : AuditableEntity
{
    private Employee(string userId,
                        string firstName,
                        string middleName,
                        string lastName,
                        DateTime birthDate,
                        string phoneNumber,
                        Department department,
                        Position position,
                        decimal salary) : base()
    {
        UserId = userId;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Department = department;
        Position = position;
        Salary = salary;
    }

    public string UserId { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public string PhoneNumber { get; private set; }
    public Department Department { get; private set; }
    public Position Position { get; private set; }
    public decimal Salary { get; private set; }

    public Result<Employee> Create(string userId,
                        string firstName,
                        string middleName,
                        string lastName,
                        DateTime birthDate,
                        string phoneNumber,
                        Department department,
                        Position position,
                        decimal salary)
    {
        return new Employee(userId,
                        firstName,
                        middleName,
                        lastName,
                        birthDate,
                        phoneNumber,
                        department,
                        position,
                        salary);
    }

    public Result<Updated> Update(string phoneNumber,
                        Department department)
    {
        PhoneNumber = phoneNumber;
        Department = department;
        return Result.Updated;
    }

    public Result<Success> Promote(Position position,
                        decimal salary)
    {
        Position = position;
        Salary = salary;
        return Result.Success;
    }
}