using eCommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var result = await _userManager.CreateAsync(new Domain.Entities.Identity.AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                NameSurname = request.NameSurname
            }, request.Password);

            var response = new CreateUserCommandResponse();

            if (result.Succeeded)
            {
                return new CreateUserCommandResponse
                {
                    Succeeded = result.Succeeded,
                    Message = "Kullanıcı başarıyla eklenmiştir"
                };
            }

            else
            {
                response.Succeeded = result.Succeeded;
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Description} - {error.Code}\n";
                }
            }
            return response;

            //var result = await _userManager.CreateAsync(new Domain.Entities.Identity.AppUser
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    UserName = request.UserName,
            //    Email = request.Email,
            //    NameSurname = request.NameSurname
            //}, request.Password);

            //var response = new CreateUserCommandResponse();
            //if (result.Succeeded)
            //{
            //    return new CreateUserCommandResponse
            //    {
            //        Succeeded = true,
            //        Message = "Kullanıcı başarıyla eklenmiştir."
            //    };
            //}

            //else
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        response.Message += $"{error.Code} - {error.Description}";
            //    }
            //}

            //return response;
        }
    }
}
