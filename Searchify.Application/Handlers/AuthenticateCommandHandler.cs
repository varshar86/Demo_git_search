using MediatR;
using Searchify.Application.Commands;
using Searchify.Domain.Interfaces;

namespace Searchify.Application.CommandHandlers;

public sealed class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, string>
{
    private readonly IAuthenticateService _authenticateService;

    public AuthenticateCommandHandler(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }

    public Task<string> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        return _authenticateService.AuthenticateUser(request.User);
    }
}
