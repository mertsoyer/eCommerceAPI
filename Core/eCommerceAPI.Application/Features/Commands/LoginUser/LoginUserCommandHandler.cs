using eCommerceAPI.Application.Abstractions.Services;
using eCommerceAPI.Application.Abstractions.Token;
using eCommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _authService.LoginService(new DTOs.User.LoginUser
            {
                Password = request.Password,
                UserNameOrEmail = request.UserNameOrEmail,
            });

            return new LoginUserCommandResponse
            {
                Message = response.Message,
                Token = response.Token
            };
        }
    }
}
