using System.Threading.Tasks;
using Akka.Actor;
using BlogApp.Models;
using BlogApp.Models.MongoDB;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BlogApp.Events
{
    public class PostDetailsEventHandler : ReceiveActor
    {
        public PostDetailsEventHandler()
        {
            ReceiveAsync<PostSaved>(Handle);
        }

        private async Task Handle(PostSaved @event)
        {
            using (var context = new MySqlDbContext())
            {

                var post = await context.Post.SingleOrDefaultAsync(
                    m => m.Id == @event.Id);

                var detailsRecord = new PostDetails
                {
                    Title = post.Title,
                    SqlId = post.Id,
                    Content = post.Content,
                    WhenCreated = post.WhenCreated
                };

                var dbContext = new MongoDBContext(); 
                await dbContext.PostDetails.InsertOneAsync(detailsRecord);

            }
        }
    }
}