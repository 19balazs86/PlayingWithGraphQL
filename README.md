# Playing with GraphQL

This is a small .NET Core application, a playground to try out GraphQL. The example just gives you a subtle insight into GraphQL.

You can find some definitions for query and mutation type in the example. Queries.txt constants all queries, which you can try in the playground.

#### Resources
- [Documentation](https://graphql-dotnet.github.io/docs/getting-started/introduction) for GraphQL for .NET.
- [GitHub](https://github.com/graphql-dotnet/graphql-dotnet) for GraphQL for .NET.
- [How to GraphQL](https://www.howtographql.com) - The FullStack Tutorial for GraphQL.
- Code Maze has a comprehensive series: [GraphQL ASP.NET Core Tutorial](https://code-maze.com/graphql-asp-net-core-tutorial/).
- C# Corner - GraphQL In .NET Core with EF: [Part 1](https://www.c-sharpcorner.com/article/graphql-in-net-core-web-api-with-entity-framework-core-part-one) | [Part 2](https://www.c-sharpcorner.com/article/graphql-in-net-core-web-api-with-entity-framework-core-part-two) (GraphQLClient).
- Medium: [Setup a GraphQL API using ASP.NET Core](https://medium.com/shemseddine-on-code/setup-a-graphql-api-using-asp-net-core-79f1b88f6ad8).
- YouTube - Max playlist - [Build a Complete App with GraphQL, Node.js, MongoDB and React.js](https://www.youtube.com/watch?v=7giZGFDGnkc&list=PL55RiY5tL51rG1x02Yyj93iypUuHYXcB).
- Custom middleware in the [graphql-dotnet/GraphQL.Harness](https://github.com/graphql-dotnet/graphql-dotnet/tree/master/src/GraphQL.Harness) repository.
- [Visual GraphQL editor](https://graphqleditor.com).

#### Sandbox
Sandbox to run queries within the browser and view API documentation.
- [GraphQL Playground](https://github.com/prisma/graphql-playground).
- [GraphiQL](https://github.com/graphql/graphiql).

#### Startup.cs

A basic setup in the Startup.cs file.

```csharp
public void ConfigureServices(IServiceCollection services)
{
  // --> GraphQL
  services
    .AddScoped<IDependencyResolver>(sp => new FuncDependencyResolver(sp.GetRequiredService))
    .AddScoped<DefinitionSchema>();

  // Add all GraphQL types (ObjectGraphType, InputObjectGraphType, ObjectGraphType<X>).
  services.AddGraphQL(options => options.ExposeExceptions = false)
    .AddGraphTypes(ServiceLifetime.Scoped);
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  // --> GraphQL
  app.UseGraphQL<DefinitionSchema>(); // Default path: /graphql
  app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); // http://localhost:5000/ui/playground
}
```
