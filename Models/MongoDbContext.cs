using System;
using BlogApp.Models.MongoDB;
using MongoDB.Driver;

namespace BlogApp.Models
{
    public class MongoDBContext 
    { 

        public IMongoDatabase _database { get; } 
 
        public MongoDBContext() 
        { 
            try 
            { 
                var mongoClient = new MongoClient("mongodb://localhost:27017"); 
                _database = mongoClient.GetDatabase("Blog"); 
            } 
            catch (Exception ex) 
            { 
                throw new Exception("Error when connecting to the server", ex); 
            } 
        } 
         public IMongoCollection<PostList> PostList
        { 
            get 
            { 
                return _database.GetCollection<PostList>("PostList"); 
            } 
        } 
         public IMongoCollection<PostDetails> PostDetails
        { 
            get 
            { 
                return _database.GetCollection<PostDetails>("PostDetails"); 
            } 
        } 
    } 
}