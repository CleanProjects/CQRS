using System.Threading.Tasks;
using Akka.Actor;
using BlogApp.Models;
using BlogApp.Models.Read;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BlogApp.Events
{
    public class PostListEventHandler : ReceiveActor
    {
        public PostListEventHandler()
        {
            ReceiveAsync<PostSaved>(Handle);
        }

        private async Task Handle(PostSaved @event)
        {
            using (var context = new MySqlDbContext())
            {

                var post = await context.Post.SingleOrDefaultAsync(
                    p => p.Id == @event.Id);

                var listRecord = new PostList
                {
                    Title = post.Title,
                    SqlId = post.Id,
                    TruncatedContent = Truncate(post.Content),
                    WhenCreated = post.WhenCreated
                };
                
                var dbContext = new MongoDBContext();
                await dbContext.PostList.InsertOneAsync(listRecord);

            }
        }

        private string Truncate(string content)
        {
            var maxLength = 120;
            if (content.Length <= maxLength)
            {
                return $"{content}...";
            } 
            else 
            {
                var truncated = content.Substring(0, maxLength);
                return $"{truncated}...";
            }
        }
    }
}