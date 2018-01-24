using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime WhenCreated { get; set; }
        public string Content { get; set; }
    }
    public class PostConfiguration: IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
        }
    }
}