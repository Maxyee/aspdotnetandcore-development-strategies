using System;

//MongoDB.Driver
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotnetCore.MongoDB
{
    // Just Declare the Field for the Student Collection
    public class Students
    {
        public ObjectId Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Age { get; set; }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Student");
            var collection = database.GetCollection<BsonDocument>("student");

            var document = new BsonDocument{
                {"Firstname", "Julhas2"},
                {"Lastname", "Hossain2"},
                {"City", "London"},
                {"Age", "26"}
            };

            collection.InsertOneAsync(document);
            Console.Read();

        }

    }
}
