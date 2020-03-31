using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
namespace dbuniver
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public String Name { get; set; }
        [BsonElement("code")]
        public String Code { get; set; }
        [BsonElement("quantity")]
        public int Quantity { get; set; }
        [BsonElement("cost")]
        public int Cost { get; set; }
        public Product(string name, string code, int quantity, int cost)
        {
            Name = name;
            Code = code;
            Quantity = quantity;
            Cost = cost;
        }
        public Product(ObjectId id, string name, string code, int quantity, int cost)
        {
            Id = id;
            Name = name;
            Code = code;
            Quantity = quantity;
            Cost = cost;
        }

        public static void insertProducr(String name, String code, int quantity, int cost)
        {
            Product product = new Product(name, code, quantity, cost);
            Form1.collection_product.InsertOne(product);
        }
        public static string sorting(List<string> list,bool type)
        {
            int t = 1;
            if (type) t = -1;

            string sort ="{" ;
            foreach(string s in list)
            {
                sort += s + ":" + t + ",";
            }
            sort = sort.Remove(sort.Length - 1);
            sort += "}";
            return sort;
        }
        public static List<Product> findProduct(String name,String sort)
        {
            var filter = new BsonDocument("$or", new BsonArray{
                                    new BsonDocument{ { "name", new BsonDocument { { "$regex", name }, { "$options", "i" } } } },
                                    new BsonDocument{ { "name", new BsonDocument { { "$regex", name }, { "$options", "i" } } } }
                        });

            if (sort.Length  == 0) return Form1.collection_product.Find(filter).ToList<Product>();

            else return Form1.collection_product.Find(filter).Sort(sort).ToList<Product>();


            /*
                         var collection = Program.db.GetCollection<BsonDocument>("product");

            var filter = new BsonDocument { { "name", new BsonDocument { { "$regex", name }, { "$options", "i" } } } };

            var result = collection.Find(filter).ToListAsync();
            Product p;
            List<Product> ps = new List<Product>();
            foreach (var r in result.Result)
            {
                p = BsonSerializer.Deserialize<Product>(r);
                ps.Add(p);
            }


            return ps;
             */


        }
        public static void updateProduct(String id, String name, String code, int quantity, int cost)
        {
            var updateDef = Builders<Product>.Update.Set("name", name).Set("code", code).Set("quantity", quantity).Set("cost", cost);
            Form1.collection_product.UpdateOne(s => s.Id == ObjectId.Parse(id), updateDef);
        }
        public static void deleteProduct(String id)
        {
            Form1.collection_product.DeleteOne(s => s.Id == ObjectId.Parse(id));
        }
        public static void updateQuantity(Product p)
        {
            Product old = Form1.collection_product.AsQueryable()
                .Where(s => s.Id == p.Id)
                .Single<Product>();
            var updateDef = Builders<Product>.Update.Set("quantity", old.Quantity - p.Quantity);
            Form1.collection_product.UpdateOne(s => s.Id == p.Id, updateDef);
        }

    }
}
