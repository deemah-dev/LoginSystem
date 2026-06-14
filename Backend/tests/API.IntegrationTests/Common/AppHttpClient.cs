using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Features.Identity;
using Application.Features.Identity.Queries.GenerateToken;
using Infrastructure.Idnetity;

namespace API.IntegrationTests.Common;

public class AppHttpClient(HttpClient httpClient)
{
    async Task<string> GenerateTokenAsync(AppUser user)
    {
        var generateTokenCommand = new GenerateTokenCommand(user.Email!, user.Email!);

        var response = await httpClient.PostAsJsonAsync("identity/token/generate", generateTokenCommand);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Token generation failed with status code {response.StatusCode}");
        }

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

        if (tokenResponse is null)
        {
            throw new InvalidOperationException("Token response is null");
        }

        return tokenResponse.AccessToken!;
    }

    public void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
    }
    public void ClearAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization = null;
    }
    public async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetAsync(requestUri, cancellationToken);
    }
    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        return await httpClient.SendAsync(request, cancellationToken);
    }

    public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value, CancellationToken cancellationToken = default)
    {
        return await httpClient.PostAsJsonAsync(requestUri, value, cancellationToken);
    }

    public async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value, CancellationToken cancellationToken = default)
    {
        return await httpClient.PutAsJsonAsync(requestUri, value, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken = default)
    {
        return await httpClient.DeleteAsync(requestUri, cancellationToken);
    }

    public async Task<HttpResponseMessage> PatchAsJsonAsync<T>(string requestUri, T value, CancellationToken cancellationToken = default)
    {
        return await httpClient.PatchAsJsonAsync(requestUri, value, cancellationToken);
    }

    public async Task<T?> GetFromJsonAsync<T>(string requestUri, CancellationToken cancellationToken = default)
    {
        var response = await GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
    }

    public async Task<T?> PostAndGetFromJsonAsync<TRequest, T>(string requestUri, TRequest value, CancellationToken cancellationToken = default)
    {
        var response = await PostAsJsonAsync(requestUri, value, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
    }

    public void Dispose()
    {
        httpClient?.Dispose();
    }

}