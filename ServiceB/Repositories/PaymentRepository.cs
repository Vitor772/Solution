using MongoDB.Driver;
using ServiceB.Models;

namespace ServiceB.Repositories
{
    public class PaymentRepository
    {
        private readonly IMongoCollection<PaymentIntention> _collection;

        public PaymentRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("PaymentsDb");
            _collection = database.GetCollection<PaymentIntention>("PaymentIntentions");
        }

        public async Task InsertAsync(PaymentIntention payment)
        {
            await _collection.InsertOneAsync(payment);
        }

        public async Task<PaymentIntention> GetByUuidAsync(string uuid)
        {
            return await _collection.Find(p => p.Uuid == uuid).FirstOrDefaultAsync();
        }
    }
}
