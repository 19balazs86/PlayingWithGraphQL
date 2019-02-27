using GraphQL.Types;
using PlayingWithGraphQL.Models;

namespace PlayingWithGraphQL.GraphQL
{
  public class OrderType : ObjectGraphType<Order>
  {
    public OrderType()
    {
      Field(x => x.Id, nullable: false).Description("Description for order id");
      Field(x => x.UserId);
      Field(x => x.Price);
      Field<DateTimeGraphType>("date", resolve: context => context.Source.Date);
      Field(x => x.Note);
    }
  }
}
