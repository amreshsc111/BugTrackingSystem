using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;

namespace BugTrackingSystem.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, Guid>
    {
        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if user exists (simplification: just checking email)
            // ideally we should check username too or use a domain service/validator
            
            // Checking logic specific to just email for now
            // Note: Since repository doesn't have specific "GetByEmail" yet, we might need to add it or fetch all (inefficient) or use Find/Predicate if Repo supports it.
            // Assuming we use standard repo, checking via direct access or needed method. 
            // For now, I'll assume usage of IUnitOfWork to get repository but Standard Repo might lack "Find".
            // However, based on previous Repository implementation, it only had GetById and GetAll.
            // I should probably skip the check or rely on DB constraint for now, or fetch all users (bad for perf but ok for prototype).
            // Actually, I'll rely on DB Unique Index on Email/Username I added in Configuration.
            
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHasher.HashPassword(request.Password)
            };

            await unitOfWork.Repository<User>().AddAsync(user);
            await unitOfWork.CompleteAsync();

            return user.Id;
        }
    }
}
