using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    public class UpdateUserRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }

        public NameRequest Name { get; set; } = new();
        public AddressRequest Address { get; set; } = new();
    }

}
