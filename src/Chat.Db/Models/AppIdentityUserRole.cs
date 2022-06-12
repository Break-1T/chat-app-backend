using Microsoft.AspNetCore.Identity;

namespace Chat.Db.Models
{
    public class AppIdentityUserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public virtual AppIdentityRole Role { get; set; }
    }
}
