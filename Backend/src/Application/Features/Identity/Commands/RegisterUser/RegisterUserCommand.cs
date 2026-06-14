using Application.Features.Identity.Dtos;
using Domain.Common.Results;
using Domain.Identity;
using MediatR;

namespace Application.Features.Identity.Commands.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Username, string Password, Role Role) : IRequest<Result<Created>>;