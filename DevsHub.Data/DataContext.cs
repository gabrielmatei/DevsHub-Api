using Microsoft.EntityFrameworkCore;

namespace DevsHub.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<TutorialCategory> TutorialCategories { get; set; }
    }
}
