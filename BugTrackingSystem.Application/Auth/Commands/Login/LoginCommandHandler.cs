using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;

namespace BugTrackingSystem.Application.Auth.Commands.Login
{
    public class LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService) : IRequestHandler<LoginCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.Repository<User>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || !passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials");
            }

            var token = jwtTokenService.GenerateToken(user);
            var refreshToken = await refreshTokenService.GenerateRefreshTokenAsync(user.Id);

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                Expiration = refreshToken.Expires
            };
        }
    }
}
