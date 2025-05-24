using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace shared.ErrorModels
{
    public class ValidationErrorToReturn
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; } = "Validation Falied";

        public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];


    }
}
