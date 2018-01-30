using System;
using MongoDB.Bson;

namespace BlogApp.Models.Read
{
    public class PostDetails
    {
        public ObjectId Id { get; set; }
        public int SqlId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime WhenCreated { get; set; }

    }
}