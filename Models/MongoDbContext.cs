using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BlogApp.Models
{
    public class MongoDBContext 
    { 

        private IMongoDatabase _database { get; } 
 
        public MongoDBContext(IConfiguration configuration) 
        { 
            var mongoSection = configuration.GetSection("MongoConnection");
            var connectionString = mongoSection.GetSection("ConnectionString").Value;
            var databaseName = mongoSection.GetSection("Database").Value;

            try 
            { 
                MongoClientSettings settings = MongoClientSettings.FromUrl(
                    new MongoUrl(connectionString)); 

                var mongoClient = new MongoClient(settings); 
                _database = mongoClient.GetDatabase(databaseName); 
            } 
            catch (Exception ex) 
            { 
                throw new Exception("Error when connecting to the server", ex); 
            } 
        } 
 
    } 
}