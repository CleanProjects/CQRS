using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogApp.Models.MongoDB
{
    public class PostDetails
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime WhenCreated { get; set; }
    }
}