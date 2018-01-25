using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using BlogApp.Models;
using BlogApp.Models.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
            // var collection = dbContext._database.GetCollection<PostList>("bar");
            // var c = dbContext.PostList;
            var result = await dbContext.PostList.FindAsync(x => true);

            // var client = new MongoClient("mongodb://localhost:27017");
            // var database = client.GetDatabase("foo");
            // var collection = database.GetCollection<PostList>("bar");
            // var result = await collection.FindAsync(x => true);
            
            Sender.Tell(result, Self);
        }

        private async Task Handle(GetPostDetails query)
        {
            // MongoDBContext dbContext = new MongoDBContext(); 
            // List<PostList> result = dbContext.PostList.Find(m => true).ToList();
            // Sender.Tell(result, Self);
        }   
    }
}