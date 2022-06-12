using Chat.Db.Models;

namespace Chat.Db.Constants
{
    /// <summary>
    /// DbConstants.
    /// </summary>
    public static class DbConstants
    {
        public static Guid MasterAdminUserId = Guid.Parse("EDC610EA-54E0-4C8F-87D1-103B4F341B6B");

        /// <summary>
        /// The user role name.
        /// </summary>
        public const string UserRoleName = "User";

        /// <summary>
        /// The master admin role name.
        /// </summary>
        public const string MasterAdminRoleName = "MasterAdmin";

        /// <summary>
        /// The user role identifier.
        /// </summary>
        public const string UserRoleId = "EB230FF7-64E2-480C-828B-C00B6917EFB5";

        /// <summary>
        /// The master admin role identifier.
        /// </summary>
        public const string MasterAdminRoleId = "9268D8A2-7B04-4BC4-88D9-5CA9643D4C3F";

        /// <summary>
        /// The roles.
        /// </summary>
        public static Dictionary<string, Guid> Roles = new Dictionary<string, Guid>
        {
            { UserRoleName, Guid.Parse(UserRoleId) },
            { MasterAdminRoleName, Guid.Parse(MasterAdminRoleId) },
        };
    }
}
