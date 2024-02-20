using eCommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
        public string Message { get; set; }
    }
}
