using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.DataTransferObjects.IdentityDTO_S
{
    public class UserDTO
    {
        [EmailAddress]
        public string Email { get; set; } = default!;

        public string Token { get; set; } = default!;

        public string DisplayName { get; set; } = default!;

    }
}
