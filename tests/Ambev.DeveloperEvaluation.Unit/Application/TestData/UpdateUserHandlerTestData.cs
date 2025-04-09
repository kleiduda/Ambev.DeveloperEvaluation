namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Dtos;
    using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
    using Ambev.DeveloperEvaluation.Domain.Enums;
    using Bogus;

    public static class UpdateUserHandlerTestData
    {
        private static readonly Faker<UpdateUserCommand> UpdateUserFaker = new Faker<UpdateUserCommand>("pt_BR")
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("119########"))
            .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
            .RuleFor(u => u.Name, f => new NameDto
            {
                Firstname = f.Name.FirstName(),
                Lastname = f.Name.LastName()
            })
            .RuleFor(u => u.Address, f => new AddressDto
            {
                City = f.Address.City(),
                Street = f.Address.StreetName(),
                Number = f.Random.Int(1, 9999),
                Zipcode = f.Address.ZipCode("#####-###"),
                Geolocation = new GeolocationDto
                {
                    Lat = f.Address.Latitude().ToString("F6"),
                    Long = f.Address.Longitude().ToString("F6")
                }
            });

        public static UpdateUserCommand GenerateValidCommand()
            => UpdateUserFaker.Generate();
    }

}
