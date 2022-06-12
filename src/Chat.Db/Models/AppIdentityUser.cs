using Microsoft.AspNetCore.Identity;

namespace Chat.Db.Models
{
    /// <summary>
    /// AppIdentityUser.
    /// </summary>
    /// <seealso cref="IdentityUser&lt;Guid&gt;" />
    public class AppIdentityUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets the record created.
        /// </summary>
        public DateTime RecCreated { get; set; }

        /// <summary>
        /// Gets or sets the record modified.
        /// </summary>
        public DateTime RecModified { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        public virtual ICollection<AppIdentityUserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the user groups.
        /// </summary>
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
