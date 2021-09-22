using System;

//MongoDB.Driver
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotnetCore.MongoDB
{
    public class Students
    {
        public ObjectId Id {get; set;}
        public string Firstname{get; set;}
        public string Lastname{get; set;}
        public string City {get; set;}
        public string Age {get;  set;}
    }

    public class Program
    {
        protected static IMangoClient _client;
        protected static IMongoDatabase _database;

        public static Students GetStudent()
        {
            Console.WriteLine("Please enter studnet first name : ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please enter student last name : ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Please enter student age : ");
            string StudentAge = console.ReadLine();

            Console.WriteLine("Please enter city name : ");
            string StudentCity = console.ReadLine();

            Students student = new Students()
            {
                Firstname = firstName,
                Lastname = lastName,
                Age = StudentAge,
                City = StudentCity,
            };

            return student;
        }

        public static void Main(string[] args)
        {
            Program p = new Program();  
            p.CRUDwithMongoDb();  
  
              
            //Hold the screen by logic  
            Console.WriteLine("Press any key to trminated the program");  
            Console.ReadKey();  
        }

        public void CRUDwithMongoDb()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("School");
            var _collection = _database.GetCollection<Students>("StudentDetails");
            Console.WriteLine("Press select your option from the following\n1 - Insert\n2 - Update One Document\n3 - Delete\n4 - Read All\n"); 
            string userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    //Insert logic
                    _collection.InsertOne(GetStudent());
                    break;

                case "2":
                    //Update logic
                    _collection.FindOneAndUpdate<Students>(
                        Builders<Students>.Filter.Eq("FirstName", obj1.FirstName),
                        Builders<Students>.Filter.Eq("LastName", obj1.LastName).Set("City", obj1.City).Set("Age", obj1.Age)); 
                    );
                    break;

                case "3":
                    //Find and Delete
                    Console.WriteLine("Please Enter the first name to delete the record(so called document) : ");  
                    var deletefirstName = Console.ReadLine();  
                    _collection.DeleteOne(s => s.FirstName == deletefirstName);
                    break;

                case "4":
                    //Read all existing document  
                    var all = _collection.Find(new BsonDocument());  
                    Console.WriteLine();  
  
                    foreach (var i in all.ToEnumerable())  
                    {  
                        Console.WriteLine(i.Id + "  " + i.FirstName + "\t" + i.LastName + "\t" + i.Age + "\t" + i.City);  
                    }  
                    break;
                
                default:  
                    Console.WriteLine("Please choose a correct option");  
                    break;  
            }

            //To continue with Program  
            Console.WriteLine("\n--------------------------------------------------------------\nPress Y for continue...\n");  
            string userChoice = Console.ReadLine();  
  
            if (userChoice == "Y" || userChoice == "y")  
            {  
                this.CRUDwithMongoDb();  
            }  
        }
    }
}
