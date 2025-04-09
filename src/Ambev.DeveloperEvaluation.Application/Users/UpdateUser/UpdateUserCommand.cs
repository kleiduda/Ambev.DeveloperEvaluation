using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Dtos;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserResult>
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }

        public NameDto Name { get; set; } = new();
        public AddressDto Address { get; set; } = new();
    }

}
