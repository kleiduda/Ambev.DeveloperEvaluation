using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Shared.TestData
{
    public static class UserTestData
    {
        private static readonly Faker<User> Faker = new Faker<User>("pt_BR")
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("119########"))
            .RuleFor(u => u.Status, f => f.PickRandom<UserStatus>())
            .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
            .RuleFor(u => u.Name, f => new Name
            {
                Firstname = f.Name.FirstName(),
                Lastname = f.Name.LastName()
            })
            .RuleFor(u => u.Address, f => new Address
            {
                City = f.Address.City(),
                Street = f.Address.StreetName(),
                Number = f.Random.Int(1, 1000),
                Zipcode = f.Address.ZipCode("#####-###"),
                Geolocation = new Geolocation
                {
                    Lat = f.Address.Latitude().ToString("F6"),
                    Long = f.Address.Longitude().ToString("F6")
                }
            });

        public static User Generate() => Faker.Generate();

        public static List<User> GenerateList(int count) => Faker.Generate(count);
    }

}
