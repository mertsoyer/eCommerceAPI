using eCommerceAPI.Application.Abstractions.Services;
using eCommerceAPI.Application.DTOs.User;
using eCommerceAPI.Application.Exceptions;
using eCommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            var result = await _userManager.CreateAsync(new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.NameSurname
            }, model.Password);

            var response = new CreateUserResponse();
            if (result.Succeeded)
            {
                return new CreateUserResponse
                {
                    Succeeded = true,
                    Message = "Login başarılı"
                };
            }
            else
            {
                response.Succeeded = result.Succeeded;
                foreach (var error in result.Errors)
                {
                    response.Message = $"{error.Code}- {error.Description}\n";
                }
                return response;
            }
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDate, int refreshTokenLifeTime)
        {
            var user = await _userManager.FindByIdAsync(appUser.Id);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(refreshTokenLifeTime);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new UserNotFoundException();

        }


    }
}
