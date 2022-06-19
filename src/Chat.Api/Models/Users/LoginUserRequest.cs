using System.Text.Json.Serialization;

namespace Chat.Api.Models.Users
{
    /// <summary>
    /// LoginUserRequest.
    /// </summary>
    public class LoginUserRequest
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
