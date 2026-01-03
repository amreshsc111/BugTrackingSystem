using BugTrackingSystem.Application.Interfaces;
using MediatR;

namespace BugTrackingSystem.Application.Auth.Commands.Logout
{
    public class LogoutCommandHandler(IRefreshTokenService refreshTokenService) : IRequestHandler<LogoutCommand, Unit>
    {
        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await refreshTokenService.RevokeRefreshTokenAsync(request.RefreshToken);
            return Unit.Value;
        }
    }
}
