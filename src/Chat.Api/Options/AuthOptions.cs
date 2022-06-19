namespace Chat.Api.Options
{
    /// <summary>
    /// AuthOptions.
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Gets or sets the token lifetime.
        /// </summary>
        /// <value>
        /// The token lifetime.
        /// </value>
        public TimeSpan TokenLifetime { get; set; }

        /// <summary>
        /// Gets or sets the remember life time.
        /// </summary>
        /// <value>
        /// The remember life time.
        /// </value>
        public int RememberLifeTime { get; set; }

        /// <summary>
        /// Gets or sets the identity server token URL.
        /// </summary>
        /// <value>
        /// The identity server token URL.
        /// </value>
        public string IdentityServerUrl { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get; set; }
    }
}
