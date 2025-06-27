using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ICacheRepository
    {
        //Get
        Task<string?> GetAsync(string CacheKey);

        //Set
        Task SetAsync(string CashKey, string Cachevalue,TimeSpan TimeToLive);

    }
}
