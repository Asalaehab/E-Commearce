using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class Authentaction(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAuthentaction
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var User=await _userManager.FindByEmailAsync(email);
            if(User is null) return false;
            return true;
        }

        public async Task<AddressDto> GetCurrentAddressAsync(string email)
        {

            var User =await _userManager.Users.Include(U => U.Address)
                .FirstOrDefaultAsync(U=>U.Email==email)?? throw new UserNotFoundException(email);

            if (User.Address is not null)
            {
                //he come as an address but he want to be returned as addressDto
                return _mapper.Map<Address, AddressDto>(User.Address);

            }
            else
            {
                throw new AddressNotFoundException(User.UserName!);
            }
        

        }

        public async Task<UserDTO> GetCurrentUserAsync(string email)
        {
            var User =await _userManager.FindByEmailAsync(email)??throw new UserNotFoundException(email);
            return new UserDTO()
            {
                DisplayName= User.DisplayName,
                Email=User.Email!,
                Token=await CreateTokenAsync(User),
            };

        }

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

        public async Task<AddressDto> UpdateCurrentUserAddress(string email, AddressDto addressDTO)
        {
            var User =await _userManager.Users.Include(U => U.Address)
                .FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundException(email);

            if(User.Address is not null)//update
            {
                User.Address.FirstName=addressDTO.FirstName;
                User.Address.LastName=addressDTO.LastName;
                User.Address.City=addressDTO.City;
                User.Address.Country=addressDTO.Country;
                User.Address.Street=addressDTO.street;

            }
            else//Add New Address
            {
                //set new Address wit the new value.
                User.Address=_mapper.Map<AddressDto,Address>(addressDTO);
            }

            await _userManager.UpdateAsync(User);

            return _mapper.Map<AddressDto>(User.Address);
            
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
