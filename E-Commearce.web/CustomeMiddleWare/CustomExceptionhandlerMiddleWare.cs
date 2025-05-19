using DomainLayer.Exceptions;
using shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commearce.web.CustomeMiddleWare
{
    public class CustomExceptionhandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionhandlerMiddleWare> _logger;

        public CustomExceptionhandlerMiddleWare(RequestDelegate Next,ILogger<CustomExceptionhandlerMiddleWare> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                if(context.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    var Response = new ErrorToReturn()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage=$"End Point {context.Request.Path} is Not Found"
                    };


                  await  context.Response.WriteAsJsonAsync(Response);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SomeThing Went Wrong");

                context.Response.StatusCode = ex switch
                {
                    NotFoundException =>StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                //set status Code for Response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //set Content type for response
                context.Response.ContentType = "application/Json";

                //response object
                var response = new ErrorToReturn()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

               
               var ResponseToReturn=JsonSerializer.Serialize(response);

               await context.Response.WriteAsync(ResponseToReturn);

                //return objext As Json

            }


        }
    }
}
