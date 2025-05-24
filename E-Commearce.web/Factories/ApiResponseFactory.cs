using Microsoft.AspNetCore.Mvc;
using shared.ErrorModels;

namespace E_Commearce.web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiErrorReponse(ActionContext Context)
        {
            var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
                   .Select(M => new ValidationError()
                   {
                       Field = M.Key,
                       Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                   });


            var Response = new ValidationErrorToReturn()
            {
                ValidationErrors = Errors
            };

            return new BadRequestObjectResult(Response);
        }
    }
}
