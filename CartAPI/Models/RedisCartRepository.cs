using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.Models
{
    public class RedisCartRepository : ICartRepository

    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public RedisCartRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteCaretAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            var data = await _database.StringGetAsync(cartId);
            if (data.IsNullOrEmpty)
                return null;
           
                return JsonConvert.DeserializeObject<Cart>(data);

        }

        public IEnumerable<string> GetUsers()
        {
            var endPoint = _redis.GetEndPoints();
            var server = _redis.GetServer(endPoint.First());
            var data = server.Keys();
            return data?.Select(k => k.ToString());
        }

        public async Task<Cart> UpdateCartAsync(Cart basket)
        {
            var created = await _database.StringSetAsync
               (basket.BuyerId, JsonConvert.SerializeObject(basket));
            if (!created)
            {
                return null;
            }
            else
            {
                return await GetCartAsync(basket.BuyerId);
            }

        }
    }
}
