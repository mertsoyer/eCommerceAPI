using eCommerceAPI.Application.Abstractions.Services;
using eCommerceAPI.Application.Abstractions.Token;
using eCommerceAPI.Application.DTOs;
using eCommerceAPI.Application.DTOs.User;
using eCommerceAPI.Application.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(ITokenHandler tokenHandler, SignInManager<Domain.Entities.Identity.AppUser> signInManager, UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginUserResponse> LoginService(LoginUser model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            }
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                var token = _tokenHandler.CreateAccessToken();
                return new LoginUserResponse
                {
                    Token = token,
                };
            }
            return new LoginUserResponse
            {
                Message = "Giriş başarısız"
            };

        }
    }
}
