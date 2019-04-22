using System.Linq;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using PlayingWithGraphQL.DataBase;
using PlayingWithGraphQL.Models;

namespace PlayingWithGraphQL.GraphQL
{
  public class UserType : ObjectGraphType<User>
  {
    public UserType(DataBaseContext dbContext)
    {
      Field(x => x.Id);
      Field(x => x.Name);
      Field(x => x.Age);
      Field<GenderEnumType>("Gender");
      Field(x => x.Company);
      Field(x => x.Email);
      Field(x => x.Registered);
      FieldAsync<ListGraphType<OrderType>>("orders",
        resolve: async context => await dbContext.Orders.Where(order => order.UserId == context.Source.Id).ToListAsync());
    }
  }
}
