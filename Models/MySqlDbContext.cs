using Microsoft.EntityFrameworkCore;

namespace BlogApp.Models
{
    public class MySqlDbContext : DbContext
    {

        public DbSet<BlogApp.Models.Post> Post { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
    }
}