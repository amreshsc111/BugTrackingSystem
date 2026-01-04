namespace BugTrackingSystem.Infrastructure.Identity
{
    public class AuthorizationPolicies
    {
        // Policy names
        public const string UserOnly = "UserOnly";
        public const string DeveloperOnly = "DeveloperOnly";
        public const string AllRoles = "AllRoles";

        // Role claim type
        public const string RoleClaimType = "role";
    }
}
