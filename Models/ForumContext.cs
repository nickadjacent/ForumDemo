using Microsoft.EntityFrameworkCore;

namespace ForumDemo.Models
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions options) : base(options) { }
        // These DbSets must be added before auto generating database with dotnet migrations
        // This DbSet corresponds to our posts table in our database which will let us query the table
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
