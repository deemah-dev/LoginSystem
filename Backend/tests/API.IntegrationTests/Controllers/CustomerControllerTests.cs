using System.Net;
using System.Net.Http.Json;
using API.IntegrationTests.Common;
using Application.Features.Interfaces;
using Application.Common.Models;
using Xunit;
using Application.Features.Customers.Dtos;

namespace API.IntegrationTests.Controllers;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CustomerControllerTests(WebAppFactory factory)
{
    private readonly AppHttpClient client = factory.CreateAppHttpClient();
    private readonly IAppDbContext context = factory.CreateAppDbContext();

    [Fact]
    async Task GetCutomers_WithValidPagination_ShouldReturnPaginatedList()
    {
        // var token = await client.GenerateTokenAsync();

        // client.SetAuthorizationHeader(token);

        var response = await client.GetAsync("/api/customers?page=1&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<PaginatedList<CustomerDto>>();
        Assert.NotNull(result);
        Assert.NotNull(result!.Items);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(10, result.PageSize);
    }
}