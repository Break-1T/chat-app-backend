using Chat.Db.Models;
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

        /// <summary>
        /// Gets or sets the user groups.
        /// </summary>
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        /// <summary>
        /// Gets or sets the chat messages.
        /// </summary>
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
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

                entity.Property(e => e.RecCreated).HasDefaultValueSql("now() at time zone 'utc'").IsRequired();
                entity.Property(e => e.RecModified).HasDefaultValueSql("now() at time zone 'utc'").IsRequired();

                entity.HasMany(u => u.ChatMessages)
                    .WithOne()
                    .HasForeignKey(e => e.GroupId);

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

                entity.HasMany(u => u.ChatMessages)
                    .WithOne()
                    .HasForeignKey(e => e.UserId);
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

            modelBuilder.Entity<ChatMessage>(entity => 
            {
                entity.ToTable("ChatMessage");

                entity.HasKey(e => e.ChatId);

                entity.Property(e => e.Message);
                entity.Property(e => e.RecCreated).HasDefaultValueSql("now() at time zone 'utc'").IsRequired();

                entity.HasIndex(e => new { e.GroupId, e.Message });
            });

            InitDefaultData.Init(modelBuilder);
        }
    }
}
