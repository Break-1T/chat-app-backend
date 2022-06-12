using Chat.Db.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Db
{
    public class ChatDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, Guid, IdentityUserClaim<Guid>, AppIdentityUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        public virtual DbSet<Group> Groups { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.HasKey(e => e.GroupId);
                entity.Property(e => e.Name);
                entity.Property(e => e.Image);

                entity.Property(e => e.RecCreated).HasDefaultValue(DateTime.UtcNow).IsRequired();
                entity.Property(e => e.RecModified).HasDefaultValue(DateTime.UtcNow).IsRequired();

                entity.HasIndex(e => e.Name);
            });

            modelBuilder.Entity<AppIdentityUser>(entity =>
            {
                entity.Property(e => e.RecCreated).HasDefaultValueSql("now() at time zone 'utc'").IsRequired();
                entity.Property(e => e.RecModified).HasDefaultValueSql("now() at time zone 'utc'").IsRequired();
                entity.Property(e => e.FirstName);
                entity.Property(e => e.LastName);
                entity.Property(e => e.Photo).HasDefaultValue(new byte[0]);

                entity.HasMany(u => u.UserRoles)
                   .WithOne()
                   .HasForeignKey(ur => ur.UserId)
                   .IsRequired();
            });

            modelBuilder.Entity<AppIdentityUserRole>(entity =>
            {
                entity.HasKey(r => new { r.UserId, r.RoleId });

                entity.HasOne(u => u.Role)
                   .WithMany()
                   .HasPrincipalKey(r => r.Id)
                   .HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.ToTable("UserGroup");

                entity.HasKey(e => new { e.UserId, e.GroupId });

                entity.HasOne(e => e.User)
                    .WithMany(e => e.UserGroups)
                    .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Group)
                    .WithMany(e => e.UserGroups)
                    .HasForeignKey(e => e.GroupId);
            });
            InitDefaultData.Init(modelBuilder);
        }
    }
}
