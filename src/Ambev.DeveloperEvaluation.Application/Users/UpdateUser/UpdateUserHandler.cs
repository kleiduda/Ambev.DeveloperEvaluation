using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserHandler(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.Id, cancellationToken);

            if (user is null)
                throw new KeyNotFoundException($"User with ID {command.Id} not found");

            user.Username = command.Username;
            user.Email = command.Email;
            user.Password = _passwordHasher.HashPassword(command.Password);
            user.Phone = command.Phone;
            user.Status = command.Status;
            user.Role = command.Role;
            user.Name = _mapper.Map<Name>(command.Name);
            user.Address = _mapper.Map<Address>(command.Address);

            await _userRepository.UpdateAsync(user, cancellationToken);

            return _mapper.Map<UpdateUserResult>(user);
        }
    }

}
