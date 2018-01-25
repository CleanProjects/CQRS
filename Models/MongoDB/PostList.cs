using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogApp.Models.MongoDB
{
    public class PostList
    {
        public ObjectId Id { get; set; }
        public int SqlId { get; set; }
        public string Title { get; set; }
        public string TruncatedContent { get; set; }
        public DateTime WhenCreated { get; set; }
    }
}