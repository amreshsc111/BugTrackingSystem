using System.ComponentModel;

namespace BugTrackingSystem.Domain.Enums
{
    public class GeneralEnums
    {
        public enum Role
        {
            [Description("Admin")]
            Admin = 1,
            [Description("Developer")]
            Developer = 2,
            [Description("Reporter")]
            Reporter = 3
        }

        public enum BugSeverity
        {
            Low = 1,
            Medium = 2,
            High = 3
        }

        public enum BugStatus
        {
            [Description("Open")]
            Open = 1,
            [Description("In Progress")]
            InProgress = 2,
            [Description("Resolved")]
            Resolved = 3,
            [Description("Closed")]
            Closed = 4
        }
    }
}
