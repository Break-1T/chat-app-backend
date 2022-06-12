using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
