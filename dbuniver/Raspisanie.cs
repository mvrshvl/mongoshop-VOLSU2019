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
    public class Raspisanie
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("sotrudnik")]
        public String Sotrudnik { get; set; }
        [BsonElement("comment")]
        public String Comment { get; set; }
        [BsonElement("status")]
        public String Status { get; set; }

        public Raspisanie(DateTime date, string sotrudnik,string comment,string status)
        {
            Date = date;
            Sotrudnik = sotrudnik;
            Comment = comment;
            Status = status;
        }

        public static List<Raspisanie> find(DateTime date)
        {
            return Form1.collection_raspisanie.Find(null)
                .ToList<Raspisanie>();
        }
        public static void insertRasp(DateTime date, String sotrudnik, String comment)
        {
            date = date.AddHours(14);
            Raspisanie rasp = new Raspisanie(date, sotrudnik, comment,"-");
            Form1.collection_raspisanie.InsertOne(rasp);
        }
        public static void updateRasp(String id, String sotrudnik, String comment)
        {
            var updateDef = Builders<Raspisanie>.Update.Set("sotrudnik",sotrudnik ).Set("comment", comment);
            Form1.collection_raspisanie.UpdateOne(s => s.Id == ObjectId.Parse(id), updateDef);
        }
        public static void deleteRasp(String id)
        {
            Form1.collection_raspisanie.DeleteOne(s => s.Id == ObjectId.Parse(id));
        }
        public static void updateStatus(DateTime date,String status)
        {
            date = date.AddHours(14);
            var updateDef = Builders<Raspisanie>.Update.Set("status", status);
            Form1.collection_raspisanie.UpdateOne(s => s.Date == date, updateDef);
        }
        public static string getStatus(DateTime date)
        {
            date = date.AddHours(14);
            var filter = new BsonDocument("date", date);
            List<Raspisanie> r = Form1.collection_raspisanie.Find(filter).ToList<Raspisanie>();
            return r.First().Status;
        }

    }
}
