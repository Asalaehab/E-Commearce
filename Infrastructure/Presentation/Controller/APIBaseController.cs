using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commearce.web.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class APIBaseController:ControllerBase
    {
        protected string GetEmailFromToken() => User.FindFirstValue(ClaimTypes.Email)!;
       
    }
}
