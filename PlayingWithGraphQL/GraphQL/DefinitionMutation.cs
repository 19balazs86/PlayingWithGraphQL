using System;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PlayingWithGraphQL.DataBase;
using PlayingWithGraphQL.Models;

namespace PlayingWithGraphQL.GraphQL
{
  public class DefinitionMutation : ObjectGraphType
  {
    public DefinitionMutation(DataBaseContext dbContext)
    {
      FieldAsync<UserType>("addUser",
        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }),
        resolve: async context =>
        {
          User user = context.GetArgument<User>("user");
          user.Registered = DateTime.Now;

          EntityEntry<User> entityEntry = dbContext.Users.Add(user);

          await dbContext.SaveChangesAsync();

          return entityEntry.Entity;
        });
    }
  }
}
