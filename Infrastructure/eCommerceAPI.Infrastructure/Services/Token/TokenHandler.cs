using eCommerceAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public Application.DTOs.Token CreateAccessToken()
        {
            var token = new Application.DTOs.Token();

            //Security Key ' in simetriği alınıyor
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak tokenın ayarları veriliyor
            token.Expiration = DateTime.UtcNow.AddMinutes(5);
            var securityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );

            //Token oluşturucu sınıftan örnek alımı 
            var tokenHandler = new JwtSecurityTokenHandler();

            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
