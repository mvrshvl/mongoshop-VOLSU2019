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
   public class Check
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public ObjectId Name { get; set; }
        [BsonElement("sum")]
        public int  Sum { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("products")]
        public  List<Product> Products { get; set; }

        public Check( ObjectId name, int sum, DateTime date, List<Product> products)
        {
            Name = name;
            Sum = sum;
            Date = date;
            Products = products;
        }
        public static void insertCheck(int sum, List<Product> products)
        {
            DateTime date = DateTime.Now;
            //date = date.AddHours(6);
            Check check = new Check(Form1.UserId, sum, date, products);
            Form1.collection_checks.InsertOne(check);
        }

        public static List<Check> findCheck(String num, DateTime date)
        {
            DateTime d1 = date.Date; d1 = d1.AddHours(6);
            DateTime d2 = date.Date; d2 = d2.AddHours(24);

            List<Check> list = Form1.collection_checks.AsQueryable()
            .Where(s => s.Date > d1)
            .Where(s => s.Date < d2)
            .ToList<Check>();

            //            var filter = new BsonDocument("$or", new BsonArray{
            //                                  new BsonDocument{ { "_id", new BsonDocument { { "$regex", num }, { "$options", "i" } } } },
            //                                new BsonDocument{ { "date", new BsonDocument { { "$regex", date }, { "$options", "i" } } } } });

            return list;

        }

        public static void updateCheck(String id,List<Product> list)
        {
            int sum = 0;
            foreach (Product p in list)
            {
                sum += p.Cost * p.Quantity;
            }
            var updateDef = Builders<Check>.Update.Set("products", list).Set("sum",sum);
            Form1.collection_checks.UpdateOne(s => s.Id == ObjectId.Parse(id), updateDef);

        }

        public static void deleteCheck(String id)
        {
            
            Form1.collection_checks.DeleteOne(s => s.Id == ObjectId.Parse(id));
        }
        public static String getSum()
        {
            int sum = 0;

            DateTime d1 = DateTime.Today.Date; d1 = d1.AddHours(6);
            DateTime d2 = DateTime.Today.Date; d2 = d2.AddHours(24);

            List<Check> list = Form1.collection_checks.AsQueryable()
            .Where(s => s.Date > d1)
            .Where(s => s.Date < d2)
            .ToList<Check>();
            foreach(Check check in list)
            {
                sum += check.Sum;
            }
            return sum.ToString();
        }
    }
}
