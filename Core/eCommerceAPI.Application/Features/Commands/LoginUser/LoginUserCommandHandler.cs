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
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            }
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {

                var token = _tokenHandler.CreateAccessToken();
                return new LoginUserCommandResponse
                {
                    Token = token
                };
            }

            return new LoginUserCommandResponse
            {
                Message = "Login olurken bir  hata oluştu"
            };


        }
    }
}
