using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peristance.Repositiories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        readonly IDatabase _database=connection.GetDatabase();
        public async Task<string?> GetAsync(string CacheKey)
        {
            var CacheValue =await _database.StringGetAsync(CacheKey);
            return CacheValue.IsNullOrEmpty ? null : CacheValue.ToString();
        }

        public async Task SetAsync(string CashKey, string Cachevalue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CashKey,Cachevalue,TimeToLive);
        }
    }
}
