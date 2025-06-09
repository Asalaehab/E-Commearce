using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using shared.DataTransferObjects.IdentityDTO_S;

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


    }
}
