using Domain.Customers;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Interfaces;

public interface IAppDbContext
{
    public DbSet<RefreshToken> RefreshTokens { get; }
    public DbSet<Customer> Customers { get; }

    Task SaveChangesAsync(CancellationToken ct);
}