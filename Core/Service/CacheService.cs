using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class CacheService(ICacheRepository cacheRepository) : ICashingService
    {
        public async Task<string?> GetAsync(string cacheKey)
        {
            return await cacheRepository.GetAsync(cacheKey);
        }

        public async Task setAsync(string cacheKey, object cachevalue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cachevalue);
            await cacheRepository.SetAsync(cacheKey, value, timeToLive);
        }
    }
}
