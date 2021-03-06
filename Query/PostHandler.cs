using System.Threading.Tasks;
using Akka.Actor;
using BlogApp.Models;
using BlogApp.Models.Read;
using MongoDB.Driver;

namespace BlogApp.Query
{
    public class PostHandler : ReceiveActor
    {
        public PostHandler()
        {
            ReceiveAsync<GetPostList>(Handle);
            ReceiveAsync<GetPostDetails>(Handle);
        }

        private async Task Handle(GetPostList query)
        {
            var dbContext = new MongoDBContext(); 
            var sort = Builders<PostList>.Sort.Descending("WhenCreated");
            var options = new FindOptions<PostList>
            {
                Sort = sort
            };

            var result = await dbContext.PostList.FindAsync(post => true, options);
            Sender.Tell(result, Self);
        }

        private async Task Handle(GetPostDetails query)
        {
            var dbContext = new MongoDBContext(); 
            var result = await dbContext.PostDetails.FindAsync(
                post => post.SqlId == query.Id
            );

            Sender.Tell(result.Single(), Self);
        }   
    }
}