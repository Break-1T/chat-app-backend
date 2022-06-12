namespace Chat.Db.Models
{
    /// <summary>
    /// Group.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Gets or sets the created by user.
        /// </summary>
        public Guid CreatedByUser { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the record created.
        /// </summary>
        public DateTime RecCreated { get; set; }

        /// <summary>
        /// Gets or sets the record modified.
        /// </summary>
        public DateTime RecModified { get; set; }

        /// <summary>
        /// Gets or sets the user groups.
        /// </summary>
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}
