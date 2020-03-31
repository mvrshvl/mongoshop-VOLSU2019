using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
namespace dbuniver
{
   public class Users
    {
     
            [BsonId]
            public ObjectId Id { get; set; }
            [BsonElement("name")]
            public String Name { get; set; }
            [BsonElement("post")]
            public String Post { get; set; }
            [BsonElement("phone")]
             public String Phone { get; set; }
            [BsonElement("login")]
            public String Login { get; set; }
            
            public Users(string name,string post,string phone, string login)
            {
                Name = name;
                Post = post;
            Phone = phone;
                Login = login;
            }
        public static List<Users> findUsers(String login)
        {
            var filter = new BsonDocument("login", login);
            return Form1.collection_users.Find(filter).ToList<Users>();
        }
        public static void updateUsers(String id, String name, String phone, String login)
        {
            var updateDef = Builders<Users>.Update.Set("name", name).Set("phone", phone).Set("post","B").Set("login", login);
            Form1.collection_users.UpdateOne(s => s.Id == ObjectId.Parse(id), updateDef);
        }
        public static void deleteUsers(String id)
        {
            Form1.collection_users.DeleteOne(s => s.Id == ObjectId.Parse(id));
        }
        public static void insertUsers(String name, String phone, String login )
        {
            Users user = new Users(name,"B", phone, login);
            Form1.collection_users.InsertOne(user);
        }
        public static void addUserDatabase(String login, String pass)
        {

                try
                {
                    var user = new BsonDocument { { "createUser", login },
                        { "pwd", pass },
                        { "roles", new BsonArray { new BsonDocument { { "role", "prodavec" },
                        { "db", "admin" } } } } };
                    new MongoClient().GetDatabase("admin").RunCommand<BsonDocument>(user);

                }
                catch (MongoCommandException) { }
                catch { }
            
        }
        public static void updatePassword(String login, String pass)
        {
            try
            {
                var user = new BsonDocument { { "createUser", login }, { "pwd", pass }, { "roles", new BsonArray { new BsonDocument { { "role", "shopAdmin" }, { "db", "shop" } } } } };
                new MongoClient().GetDatabase("admin").RunCommand<BsonDocument>(user);
            }
            catch
            {

            }
        }
    }
}
