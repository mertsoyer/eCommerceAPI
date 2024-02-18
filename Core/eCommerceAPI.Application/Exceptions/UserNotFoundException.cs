using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException():base("Kullanıcı adı veya şifre hatalı")
        {
                
        }
        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
