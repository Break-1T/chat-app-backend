using Chat.Db.Constants;
using Chat.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Db
{
    public static class InitDefaultData
    {
        private const string Stamp = "8787CD7E-D016-4E77-8058-36BFE8226E75";
        private const string AdminEmail = "admin@admin.com";
        private const string AdminPassword = "ABCD1234abcd@";

        /// <summary>
        /// Initializes the default data.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void Init(ModelBuilder modelBuilder)
        {
            var normalizer = new UpperInvariantLookupNormalizer();
            var hasher = new PasswordHasher<IdentityUser>();

            var identityRoles = DbConstants.Roles.Select(role =>
                new AppIdentityRole
                {
                    Id = role.Value,
                    Name = role.Key,
                    NormalizedName = normalizer.NormalizeName(role.Key),
                    ConcurrencyStamp = Stamp,
                });

            modelBuilder.Entity<AppIdentityRole>().HasData(identityRoles);

            modelBuilder.Entity<AppIdentityUser>().HasData(
               new AppIdentityUser
               {
                   Id = DbConstants.MasterAdminUserId,
                   UserName = AdminEmail,
                   NormalizedUserName = normalizer.NormalizeName(AdminEmail),
                   PasswordHash = hasher.HashPassword(null, AdminPassword),
                   EmailConfirmed = true,
                   Email = AdminEmail,
                   NormalizedEmail = normalizer.NormalizeEmail(AdminEmail),
                   ConcurrencyStamp = Stamp,
                   SecurityStamp = Stamp,
               });

            modelBuilder.Entity<AppIdentityUserRole>().HasData(
                new AppIdentityUserRole
                {
                    RoleId = identityRoles.First(e => e.Name == DbConstants.MasterAdminRoleName).Id,
                    UserId = DbConstants.MasterAdminUserId,
                });
        }
    }
}
