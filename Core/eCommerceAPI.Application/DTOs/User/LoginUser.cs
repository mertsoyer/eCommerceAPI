using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.DTOs.User
{
    public class LoginUser
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
