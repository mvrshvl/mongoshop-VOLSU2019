using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace dbuniver
{
    public class Card
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public String Name { get; set; }
        [BsonElement("phone")]
        public String Phone { get; set; }

        public Card(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
}
