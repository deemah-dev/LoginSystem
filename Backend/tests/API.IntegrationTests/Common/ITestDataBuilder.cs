using Domain.Customers;

namespace API.IntegrationTests.Common;

public interface ITestDataBuilder<T>
{
    T Build();
}
public class CustomerTestDataBuilder : ITestDataBuilder<Customer>
{
    public Customer Build()
    {
        throw new NotImplementedException();
    }
}