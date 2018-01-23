using Microsoft.EntityFrameworkCore;

namespace BlogApp.Models
{
    public class MySqlDbContext : DbContext
    {

        public DbSet<BlogApp.Models.Post> Post { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseMySql(@"Server=localhost;database=blog;uid=root;pwd=password;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
    }
}