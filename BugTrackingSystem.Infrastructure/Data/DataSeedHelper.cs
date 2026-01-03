using BugTrackingSystem.Domain.Entities;

namespace BugTrackingSystem.Infrastructure.Data
{
    public class DataSeedHelper(ApplicationDbContext dbContext)
    {
        public void InsertData()
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.AddRange(
                [
                    new Role { Name = "Admin", Description = "Admin" },
                    new Role { Name = "Developer", Description = "Developer" },
                    new Role { Name = "Reporter", Description = "Reporter" }
                ]);
                dbContext.SaveChanges();
            }
        }
    }
}
