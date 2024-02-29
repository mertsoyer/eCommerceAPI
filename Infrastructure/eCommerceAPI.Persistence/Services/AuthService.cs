using eCommerceAPI.Application.Abstractions.Services;
using eCommerceAPI.Application.Abstractions.Token;
using eCommerceAPI.Application.DTOs;
using eCommerceAPI.Application.DTOs.User;
using eCommerceAPI.Application.Exceptions;
using eCommerceAPI.Domain.Entities.Identity;
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
        private readonly IUserService _userService;

        public AuthService(ITokenHandler tokenHandler, SignInManager<Domain.Entities.Identity.AppUser> signInManager, UserManager<Domain.Entities.Identity.AppUser> userManager, IUserService userService)
        {
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<LoginUserResponse> LoginAsync(LoginUser model, int accessTokenLifeTime)
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
                var token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10);
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

        //Üstteki metotda LoginUserResponse üzerinden token döndürdük, RefreshTokenLogin de ise direkt token döndürdük. Farklı kullanım açısından...
        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            ///Refresh token değeri üzerinden db de ki kullanıcı bulunur
            var user = _userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);

            //kullanıcıya ait gelen refreshtokenEndDate süresi geçerliyse tekrardan yeni accesstoken ve refresh token oluşturulur.
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                var token = _tokenHandler.CreateAccessToken(5,user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10);
                return token;
            }
            else
                throw new UserNotFoundException();

        }
    }
}
