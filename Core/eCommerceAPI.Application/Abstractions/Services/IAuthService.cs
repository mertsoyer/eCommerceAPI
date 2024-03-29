﻿using eCommerceAPI.Application.DTOs;
using eCommerceAPI.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<LoginUserResponse> LoginAsync(LoginUser user, int accessTokenLifeTime);
        Task<Application.DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
