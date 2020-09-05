using System;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlayingWithGraphQL.DataBase;

namespace PlayingWithGraphQL.GraphQL
{
  public class DefinitionQuery : ObjectGraphType
  {
    private static readonly Random _random = new Random();

    private readonly DataBaseContext _dbContext;
    private readonly ILogger<DefinitionQuery> _logger;

    public DefinitionQuery(DataBaseContext dbContext, ILogger<DefinitionQuery> logger)
    {
      _dbContext = dbContext;
      _logger    = logger;

      // Define fields.
      FieldAsync<ListGraphType<OrderType>>("orders",
        arguments: new QueryArguments(
          new QueryArgument<IntGraphType>() { Name = "skip", DefaultValue = 0 },
          new QueryArgument<IntGraphType>() { Name = "take", DefaultValue = int.MaxValue }),
        resolve: resolveOrders);

      FieldAsync<ListGraphType<UserType>>("users",
        resolve: async _ => await dbContext.Users.ToListAsync());

      FieldAsync<UserType>("user",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>>() { Name = "id", Description = "User id" }),
        resolve: async context => await dbContext.Users
          .FirstOrDefaultAsync(u => u.Id == context.GetArgument<int>("id", 0)));
    }

    private async Task<object> resolveOrders(IResolveFieldContext<object> context)
    {
      try
      {
        // Throw a random exception.
        if (_random.NextDouble() < 0.2)
          throw new Exception("Random Exception");

        // Check skip value and add ValidationError.
        if (context.HasArgument("skip") && context.GetArgument<int>("skip") < 0)
        {
          context.Errors.Add(new ValidationError(
            context.Document.OriginalQuery, "skip", "The argument 'skip' can not be less than 0"));
          return null;
        }

        // Return with the list of orders.
        return await _dbContext
          .Orders
          .Skip(context.GetArgument<int>("skip"))
          .Take(context.GetArgument<int>("take"))
          .ToListAsync();
      }
      catch (Exception ex)
      {
        string errorMessage = $"Failed to get the orders: {ex.Message}.";

        context.Errors.Add(new ExecutionError(errorMessage, ex));

        _logger.LogError(errorMessage, ex);
      }

      return null;
    }
  }
}
