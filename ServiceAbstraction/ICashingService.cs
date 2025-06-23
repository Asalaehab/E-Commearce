using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICashingService
    {
        Task<string?> GetAsync(string cacheKey);
        Task setAsync(string cacheKey, object cachevalue,TimeSpan timeToLive);
    }
}
