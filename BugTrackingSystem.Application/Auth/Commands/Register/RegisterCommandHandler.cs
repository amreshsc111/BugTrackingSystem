using BugTrackingSystem.Application.Exceptions;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;

namespace BugTrackingSystem.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, Guid>
    {
        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if username already exists
            var existingUserByUsername = await unitOfWork.Repository<User>()
                .FindOneAsync(u => u.UserName == request.UserName);
            if (existingUserByUsername != null)
            {
                throw new ValidationException("UserName", "Username already exists.");
            }

            // Check if email already exists
            var existingUserByEmail = await unitOfWork.Repository<User>()
                .FindOneAsync(u => u.Email == request.Email);
            if (existingUserByEmail != null)
            {
                throw new ValidationException("Email", "Email already exists.");
            }

            // Verify role exists
            var role = await unitOfWork.Repository<Role>()
                .FindOneAsync(r => r.Id == request.RoleId);
            if (role == null)
            {
                throw new ValidationException("RoleId", "Invalid role.");
            }

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHasher.HashPassword(request.Password),
                CreatedById = Guid.Empty, // System created
                CreatedDate = DateTime.UtcNow
            };

            await unitOfWork.Repository<User>().AddAsync(user);
            await unitOfWork.CompleteAsync();

            // Assign role to user
            user.Roles.Add(role);
            await unitOfWork.CompleteAsync();

            return user.Id;
        }
    }
}
