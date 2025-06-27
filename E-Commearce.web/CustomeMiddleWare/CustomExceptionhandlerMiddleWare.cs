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
       
                _logger.LogError(ex, "Something Went Wrong");

                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized, // use 401 for unauthorized
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.ContentType = "application/json";

                var response = new ErrorToReturn
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                var responseToReturn = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(responseToReturn);
            }


        


    }
    }
}
