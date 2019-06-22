using System;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using PlayingWithGraphQL.Models;

namespace PlayingWithGraphQL.DataBase
{
  public static class DataBaseInitializer
  {
    public static void SeedData(this DataBaseContext context)
    {
      context.Database.EnsureCreated();

      if (context.Users.Any() || context.Orders.Any()) return;

      var users = new Faker<User>()
        //.RuleFor(p => p.Id, _ => id++) // No need. The auto increment works well.
        .RuleFor(p => p.Age, f => f.Random.Number(15, 65))
        .RuleFor(p => p.Gender, f => f.PickRandom<Gender>())
        .RuleFor(p => p.Name, (f, u) => f.Name.FullName(u.Gender == Gender.Man ? Name.Gender.Male : Name.Gender.Female))
        .RuleFor(p => p.Company, f => f.Company.CompanyName())
        .RuleFor(p => p.Email, f => f.Internet.Email())
        .RuleFor(p => p.Registered, f => f.Date.Between(DateTime.Now.AddYears(30), DateTime.Now))
        .Generate(30);

      var orders = new Faker<Order>()
        .RuleFor(p => p.User, f => f.PickRandom(users))
        .RuleFor(p => p.Price, f => f.Random.Double(50, 5000))
        .RuleFor(p => p.Date, (f, r) => f.Date.Between(r.User.Registered, DateTime.Now))
        .RuleFor(p => p.Note, f => f.Lorem.Sentence())
        .Generate(100);

      context.Users.AddRange(users);
      context.Orders.AddRange(orders);

      context.SaveChanges();
    }
  }
}
