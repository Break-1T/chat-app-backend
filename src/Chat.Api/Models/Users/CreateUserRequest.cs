using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Chat.Api.Models.Users
{
    /// <summary>
    /// CreateUserRequest.
    /// </summary>
    public class CreateUserRequest : User
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
