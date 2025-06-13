using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using shared.DataTransferObjects.IdentityDTO_S;
using System.Security.Claims;

namespace E_Commearce.web.Controllers
{
    public class AuthentactionController(IServiceManager _serviceManager):APIBaseController
    {
        //Login
        [HttpPost("Login")]//Post BaseUrl
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
          var User= await _serviceManager.Authentaction.LoginAsync(loginDTO);

            return Ok(User);
        }


        //Register


        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
        {
            var User=await   _serviceManager.Authentaction.RegisterAsync(registerDto);
            return Ok(User);
        }

        //check Email
        [HttpGet("CheckEmail")]//Get BaseUrl/api/Authentication/CheckEmail

        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result =await _serviceManager.Authentaction.CheckEmailAsync(email);
            return Ok(Result);
        }

        //Get Current User
        [Authorize]//inside token we send email
        [HttpGet("CurrentUser")] //Get BaseUrl/api/Authentaction/checkEmail
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
           var email=User.FindFirstValue(ClaimTypes.Email);
            var AppUer =await _serviceManager.Authentaction.GetCurrentUserAsync(email!);
            return Ok(AppUer);
        }

        //Get Current User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address =await _serviceManager.Authentaction.GetCurrentAddressAsync(email!);
            return Ok(Address);
        }

        //Update Current User Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>>UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress =await _serviceManager.Authentaction.UpdateCurrentUserAddress(email!, addressDto);
            return Ok(UpdatedAddress);
        }
    }
}
