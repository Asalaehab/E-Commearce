using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class UserNotFoundException(string Email) : NotFoundException($"the user with {Email} is Not Found ! ")
    {
    }
}
