using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.DTOs.User
{
    public class LoginUserResponse
    {
        public Token Token { get; set; }
        public string Message { get; set; }
    }
}
