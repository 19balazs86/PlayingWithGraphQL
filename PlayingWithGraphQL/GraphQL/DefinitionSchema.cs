using System;
using GraphQL.Types;
using GraphQL.Utilities;

namespace PlayingWithGraphQL.GraphQL
{
  public class DefinitionSchema : Schema
  {
    public DefinitionSchema(IServiceProvider provider) : base(provider)
    {
      Query    = provider.GetRequiredService<DefinitionQuery>();
      Mutation = provider.GetRequiredService<DefinitionMutation>();
    }
  }
}
