using MediatR;
using Searchify.Domain.Model;

namespace Searchify.Application.Commands;

public sealed record AuthenticateCommand(User User) : IRequest<string>;
