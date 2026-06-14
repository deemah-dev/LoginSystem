using Application.Features.Customers.Commands.CreateCustomer;

namespace Application.SubcutaneousTests.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidatorTests
{
    private readonly CreateCustomerCommandValidator validator;

    public CreateCustomerCommandValidatorTests()
    {
        validator = new CreateCustomerCommandValidator();
    }
}