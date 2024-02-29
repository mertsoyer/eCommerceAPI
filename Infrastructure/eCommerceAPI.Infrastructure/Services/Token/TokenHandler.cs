using eCommerceAPI.Application.Abstractions.Token;
using eCommerceAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int accessTokenLifeTime, AppUser user)
        {
            var token = new Application.DTOs.Token();

            //Security Key ' in simetriği alınıyor
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak tokenın ayarları veriliyor
            token.Expiration = DateTime.UtcNow.AddMinutes(accessTokenLifeTime);
            var securityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
                );

            //Token oluşturucu sınıftan örnek alımı 
            var tokenHandler = new JwtSecurityTokenHandler();

            token.AccessToken = tokenHandler.WriteToken(securityToken);

            //var refreshToken = CreateRefreshToken();
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            var number = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
            };
            return Convert.ToBase64String(number);
        }
    }
}
