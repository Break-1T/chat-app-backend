namespace Chat.Db.Models
{
    /// <summary>
    /// Linking table for many to many relationship User <-> Group.
    /// </summary>
    public class UserGroup
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public AppIdentityUser User { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        public Group Group { get; set; }
    }
}
