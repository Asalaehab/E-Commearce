using shared.DataTransferObjects.IdentityDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceAbstraction
{
    public interface IAuthentaction
    {
        //Login
        //Take Email and Password Then Return Token ,  Email and DisplayName To Client  
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);

        //Registration
        //Will Take Email, Password, UserName, Display Name And Phone Number Then Return Token , Email and Display Name To Client
        Task<UserDTO> RegisterAsync(RegisterDto registerDTO);

    }
}
