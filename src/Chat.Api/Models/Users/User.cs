using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Chat.Api.Models.Users
{
    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the photo in byte[] array.
        /// </summary>
        [JsonPropertyName("photo")]
        public byte[]? Photo { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Required]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }
    }
}
