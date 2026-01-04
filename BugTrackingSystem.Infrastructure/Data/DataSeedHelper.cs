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
                    new Role { Name = "User", Description = "User" },
                    new Role { Name = "Developer", Description = "Developer" }
                ]);
                dbContext.SaveChanges();
            }

            if (!dbContext.SeverityLevels.Any())
            {
                dbContext.SeverityLevels.AddRange(
                [
                    new SeverityLevel { Name = "Low", Description = "Low" },
                    new SeverityLevel { Name = "Medium", Description = "Medium" },
                    new SeverityLevel { Name = "High", Description = "High" }
                ]);
                dbContext.SaveChanges();
            }
        }
    }
}
