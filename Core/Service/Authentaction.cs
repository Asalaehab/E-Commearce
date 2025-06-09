using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using shared.DataTransferObjects.IdentityDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Authentaction(UserManager<ApplicationUser> _userManager) : IAuthentaction
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            //check if email is existed or not
            var User=await _userManager.FindByEmailAsync(loginDTO.Email);

            if(User is null) throw new UserNotFoundException(loginDTO.Email);
            //check password
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User,loginDTO.Password);

            if (IsPasswordValid)
            {
                return new UserDTO()
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token = CreateTokenAsync(User)

                }; 
            }
            else
                throw new UnauthorizedException();
            
            
            //return UserDto
        }

        public async Task<UserDTO> RegisterAsync(RegisterDto registerDTO)
        {
            //Mapping RegisterDto => ApplicationUser
            var User = new ApplicationUser()
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.UserName,
            };
            var Result =await _userManager.CreateAsync(User, registerDTO.Password);

            if(Result.Succeeded)
            {
                return new UserDTO()
                {
                    Email = registerDTO.Email,
                    DisplayName = registerDTO.DisplayName,
                    Token = CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors=Result.Errors.Select(E=>E.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }

        private static string CreateTokenAsync(ApplicationUser User)
        {
            return "TODO:)";
        }
    }
}
