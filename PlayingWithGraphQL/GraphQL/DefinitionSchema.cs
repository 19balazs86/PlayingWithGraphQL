using GraphQL;
using GraphQL.Types;

namespace PlayingWithGraphQL.GraphQL
{
  public class DefinitionSchema : Schema
  {
    public DefinitionSchema(IDependencyResolver resolver) : base(resolver)
    {
      Query    = resolver.Resolve<DefinitionQuery>();
      Mutation = resolver.Resolve<DefinitionMutation>();
    }
  }
}
