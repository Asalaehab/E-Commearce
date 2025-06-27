using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{

    class CacheAttribute(int DurationInSec=90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Create Cache Key
            string cacheKey =CreateCacheKey(context.HttpContext.Request);

            //Search For Value With cache Key
            ICashingService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICashingService>();
            var cacheValue=await cacheService.GetAsync(cacheKey);


            //Return Value If Not Null
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType="application/json",
                    StatusCode=StatusCodes.Status200OK
                };
                return;
            }


            //Return Value if Is Null
            var ExcutedContext=await next.Invoke();

            if (ExcutedContext.Result is OkObjectResult result)
            {
              await  cacheService.setAsync(cacheKey, result.Value!, TimeSpan.FromSeconds(DurationInSec));
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
           StringBuilder Key = new StringBuilder();
            Key.Append(request.Path +'?'); 
            foreach(var Item in request.Query.OrderBy(Q=>Q.Key))
            {
                Key.Append($"{Item.Key}={Item.Value}&");
            }
            return Key.ToString();
        }
    }
}
