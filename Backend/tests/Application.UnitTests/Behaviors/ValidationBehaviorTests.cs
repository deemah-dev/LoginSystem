using Application.Common.Behaviors;
using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Dtos;
using Application.Features.Customers.Mappers;
using Domain.Common.Results;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Tests.Common.Customers;
using Xunit;

namespace Application.UnitTests.Behaviors;

public class ValidationBehaviorTests
{
    private readonly IValidator<CreateCustomerCommand> mockValidator;
    private readonly RequestHandlerDelegate<Result<CustomerDto>> mockNext;

    private readonly ValidationBehavior<CreateCustomerCommand, Result<CustomerDto>> behavior;
    public ValidationBehaviorTests()
    {
        mockValidator = Substitute.For<IValidator<CreateCustomerCommand>>();
        mockNext = Substitute.For<RequestHandlerDelegate<Result<CustomerDto>>>();

        behavior = new(mockValidator);
    }

    [Fact]
    async Task InvokeValidationBehavior_WhanValidatorResultIsValid_ShouldInvokeNextBehavior()
    {
        var command = CustomerCommandFactory.Create_CreateCustomerCommand();
        var response = CustomerFactory.CreateCustomer().Value.ToDto();

        mockValidator
        .ValidateAsync(command, Arg.Any<CancellationToken>())
        .Returns(new ValidationResult());

        mockNext
        .Invoke()
        .Returns(response);

        var behaviorResult = await behavior.Handle(command, mockNext, default);

        Assert.True(behaviorResult.IsSuccess);
        Assert.Equal(response, behaviorResult.Value);
    }
    [Fact]
    async Task InvokeValidationBehavior_WhanValidatorResultIsNotValid_ShouldReturnsErrorsList()
    {
        var command = CustomerCommandFactory.Create_CreateCustomerCommand();
        List<ValidationFailure> validationFailures = [new("propertyName1", "errorMessage1")];

        mockValidator
        .ValidateAsync(command, Arg.Any<CancellationToken>())
        .Returns(new ValidationResult(validationFailures));

        var behaviorResult = await behavior.Handle(command, mockNext, default);

        Assert.True(behaviorResult.IsError);
        Assert.Equal(validationFailures[0].PropertyName, behaviorResult.TopError.Code);
        Assert.Equal(validationFailures[0].ErrorMessage, behaviorResult.TopError.Description);
    }
    [Fact]
    async Task InvokeValidationBehavior_WhanNoValidator_ShouldInvokeNextBehavior()
    {
        var command = CustomerCommandFactory.Create_CreateCustomerCommand();
        var response = CustomerFactory.CreateCustomer().Value.ToDto();

        var behavior = new ValidationBehavior<CreateCustomerCommand, Result<CustomerDto>>(null);

        mockNext
        .Invoke()
        .Returns(response);

        var behaviorResult = await behavior.Handle(command, mockNext, default);

        Assert.True(behaviorResult.IsSuccess);
        Assert.Equal(response, behaviorResult.Value);
    }


}