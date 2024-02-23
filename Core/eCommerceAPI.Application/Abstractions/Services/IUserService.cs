using eCommerceAPI.Application.DTOs.User;
using eCommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="appUser"></param>
        /// <param name="accessTokenDate">Mevcut access token değeri</param>
        /// <param name="addToAccessTokenDate">Access token üzerine eklenecek refresh token değeri</param>
        /// <returns></returns>
        Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDate, int addToAccessTokenDate);

    }
}
