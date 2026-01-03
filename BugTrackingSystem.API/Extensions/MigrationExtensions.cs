using BugTrackingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            if (app.Configuration.GetValue<bool>("MigrationConfig:IsAutoMigration"))
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
