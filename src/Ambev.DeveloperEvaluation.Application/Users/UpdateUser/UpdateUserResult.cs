﻿using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Dtos;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    public class UpdateUserResult
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }

        public NameDto Name { get; set; } = new();
        public AddressDto Address { get; set; } = new();
    }

}
