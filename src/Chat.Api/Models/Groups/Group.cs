using Chat.Api.Models.Users;
using System.Text.Json.Serialization;

namespace Chat.Api.Models.Groups
{
    /// <summary>
    /// Group.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        [JsonPropertyName("group_id")]
        public Guid? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        [JsonPropertyName("group_name")]
        public string? GroupName { get; set; }

        /// <summary>
        /// Gets or sets the group image.
        /// </summary>
        [JsonPropertyName("group_image")]
        public byte[]? GroupImage { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        [JsonPropertyName("users")]
        public List<User> Users { get; set; }
    }
}
