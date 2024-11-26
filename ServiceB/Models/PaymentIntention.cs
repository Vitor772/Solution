using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceB.Models
{
    public class PaymentIntention
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Uuid { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; } // "Pending", "Success", "Failed"
    }
}
