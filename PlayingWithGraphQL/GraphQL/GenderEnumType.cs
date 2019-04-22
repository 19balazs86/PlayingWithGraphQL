using GraphQL.Types;
using PlayingWithGraphQL.Models;

namespace PlayingWithGraphQL.GraphQL
{
  public class GenderEnumType : EnumerationGraphType<Gender>
  {
    public GenderEnumType()
    {
      Name        = "Gender";
      Description = "Enumeration for the gender.";
    }
  }
}
