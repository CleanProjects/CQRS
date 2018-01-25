using System;
using System.Threading.Tasks;
using Akka.Actor;
using BlogApp.Models;
using BlogApp.Models.MongoDB;
using BlogApp.Models.MySQL;
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

                var post = await context.Post.SingleOrDefaultAsync(m => m.Id == @event.Id);

                // TODO: what if len of Content is less than 10 chars?
                var listRecord = new PostList
                {
                    Title = post.Title,
                    TruncatedContent = post.Content.Substring(0, 10),
                    WhenCreated = post.WhenCreated
                };
                
                var dbContext = new MongoDBContext();
                await dbContext.PostList.InsertOneAsync(listRecord);

            }
        }
    }
}