namespace BugTrackingSystem.Infrastructure.Identity
{
    public class AuthorizationPolicies
    {
        // Policy names
        public const string AdminOnly = "AdminOnly";
        public const string DeveloperOnly = "DeveloperOnly";
        public const string AllRoles = "AllRoles";

        // Role claim type
        public const string RoleClaimType = "role";
    }
}
