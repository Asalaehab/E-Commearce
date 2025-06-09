using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using shared.DataTransferObjects.IdentityDTO_S;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Authentaction(UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IAuthentaction
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
                    Email = User.Email!,
                    DisplayName = User.DisplayName,
                    Token =await CreateTokenAsync(User)

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
                    Token =await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors=Result.Errors.Select(E=>E.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }

        private async Task<string> CreateTokenAsync(ApplicationUser User)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,User.Email!),
                new(ClaimTypes.Name,User.UserName!),
                new(ClaimTypes.NameIdentifier,User.Id),

            };
            var Roles =await _userManager.GetRolesAsync(User);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];

            var Key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));

            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims:Claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:Creds
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
