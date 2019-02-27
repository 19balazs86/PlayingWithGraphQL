using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PlayingWithGraphQL.Models;

namespace PlayingWithGraphQL.DataBase
{
  public static class DataBaseInitializer
  {
    public static void SeedData(this DataBaseContext context)
    {
      context.Database.EnsureCreated();

      if (context.Users.Any() || context.Orders.Any()) return;

      context.Users.AddRange(JsonConvert.DeserializeObject<IEnumerable<User>>(File.ReadAllText("db.users.json")));
      context.Orders.AddRange(JsonConvert.DeserializeObject<IEnumerable<Order>>(File.ReadAllText("db.orders.json")));

      context.SaveChanges();
    }
  }
}
