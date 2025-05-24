using DomainLayer.Models.BasketModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Peristance.Repositiories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeLive = null)
        {
            var JsonBasket= JsonSerializer.Serialize(basket);

            var IsCreated=await _database.StringSetAsync(basket.Id, JsonBasket, timeLive??TimeSpan.FromDays(30));

            if (IsCreated)
                return await GetBasketAsync(basket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string id)=>  await _database.KeyDeleteAsync(id);


        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var Basket=await _database.StringGetAsync(id);

            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
