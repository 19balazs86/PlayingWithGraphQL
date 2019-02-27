using GraphQL.Types;

namespace PlayingWithGraphQL.GraphQL
{
  public class UserInputType : InputObjectGraphType
  {
    public UserInputType()
    {
      Name = "MyUserInputType";

      Field<NonNullGraphType<StringGraphType>>("name");
      Field<NonNullGraphType<IntGraphType>>("age");
      Field<NonNullGraphType<StringGraphType>>("company");
      Field<NonNullGraphType<StringGraphType>>("email");
    }
  }
}
