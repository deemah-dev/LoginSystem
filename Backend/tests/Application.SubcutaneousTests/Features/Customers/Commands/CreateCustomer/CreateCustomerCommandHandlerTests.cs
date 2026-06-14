using Application.Features.Interfaces;
using Application.SubcutaneousTests.Common;
using MediatR;
using Tests.Common.Customers;
using Xunit;

namespace Application.SubcutaneousTests.Features.Customers.Commands.CreateCustomer;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateCustomerCommandHandlerTests(WebAppFactory factory)
{
    private readonly IMediator mediator = factory.CreateMediator();
    private readonly IAppDbContext context = factory.CreateAppDbContext();

    [Fact]
    async Task Handle_WithValidData_ShouldSucceed()
    {
        var customer = CustomerFactory.CreateCustomer().Value;

        await context.Customers.AddAsync(customer);

        await context.SaveChangesAsync(default);

        var command = CustomerCommandFactory.Create_CreateCustomerCommand();

        var result = await mediator.Send(command);

        Assert.True(result.IsSuccess);
        //Assert.Equal(customer.Id, result.Value.Id);
        Assert.Equal(customer.Address.Country, result.Value.Address.Country);
        Assert.Equal(customer.Address.City, result.Value.Address.City);
        Assert.Equal(customer.Address.Street, result.Value.Address.Street);
        Assert.Equal(customer.BirthDate, result.Value.BirthDate);
        Assert.Equal(customer.Gender, result.Value.Gender);
        Assert.Equal(customer.PhoneNumber, result.Value.PhoneNumber);
        Assert.Equal(customer.UserId, result.Value.UserId);
        Assert.Equal($"{customer.FirstName} {customer.MiddleName} {customer.LastName}", result.Value.FullName);
    }
}