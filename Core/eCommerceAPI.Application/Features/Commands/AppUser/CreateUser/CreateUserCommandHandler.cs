using eCommerceAPI.Application.Abstractions.Services;
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
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {

            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var response = await _userService.CreateAsync(new DTOs.User.CreateUser
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                UserName = request.UserName,
            });

            return new CreateUserCommandResponse
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };
        }
    }
}
