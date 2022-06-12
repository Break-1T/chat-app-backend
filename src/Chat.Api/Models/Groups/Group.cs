using Chat.Api.Models.Users;

namespace Chat.Api.Models.Groups
{
    /// <summary>
    /// Group.
    /// </summary>
    public class Group
    {
        public Guid? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        public string? GroupName { get; set; }

        /// <summary>
        /// Gets or sets the group image.
        /// </summary>
        public byte[]? GroupImage { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public List<User> Users { get; set; }
    }
}
