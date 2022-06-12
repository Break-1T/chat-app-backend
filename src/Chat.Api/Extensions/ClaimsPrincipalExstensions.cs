using Chat.Api.Constants;
using System.Security.Claims;

namespace Chat.Api.Extensions
{
    public static class ClaimsPrincipalExstensions
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns>UserId.</returns>
        /// <exception cref="ArgumentNullException">claimsPrincipal.</exception>
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            //var value = claimsPrincipal.GetClaimValue(ApiConstant.ApiClaims.UserId);
            var value = claimsPrincipal.GetClaimValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            return Guid.Parse(value);
        }

        /// <summary>
        /// Gets the session identifier.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns>SessionId.</returns>
        /// <exception cref="ArgumentNullException">claimsPrincipal.</exception>
        public static Guid? GetSessionId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            var value = claimsPrincipal.GetClaimValue(ApiConstant.ApiClaims.GroupIdClaimType);

            return Guid.TryParse(value, out var id) ? id : (Guid?)null;
        }

        /// <summary>
        /// Gets the claim value.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="claimName">Name of the claim.</param>
        /// <returns>ClaimValue.</returns>
        /// <exception cref="ArgumentNullException">claimsPrincipal.</exception>
        /// <exception cref="ArgumentException">Value is null or empty. - claimName.</exception>
        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimName)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            if (string.IsNullOrEmpty(claimName))
            {
                throw new ArgumentException($"Value is null or empty. ", nameof(claimName));
            }

            var cl = claimsPrincipal.Claims.FirstOrDefault(cl => cl.Type.Equals(claimName, StringComparison.InvariantCultureIgnoreCase));

            return claimsPrincipal.Claims.FirstOrDefault(cl => cl.Type.Equals(claimName, StringComparison.InvariantCultureIgnoreCase))?.Value;
        }
    }
}
