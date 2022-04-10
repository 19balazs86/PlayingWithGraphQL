using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using PlayingWithGraphQL.DataBase;
using PlayingWithGraphQL.GraphQL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// ----> ConfigureServices <----
IServiceCollection services = builder.Services;

// --> EF: Use in-memory database.
services.AddDbContext<DataBaseContext>(options => options.UseInMemoryDatabase("dbName"));

services.AddScoped<ISchema, DefinitionSchema>();

builder.Services
  .AddGraphQL(options =>
  {
    options.EnableMetrics = true;
  })
  .AddSystemTextJson()
  .AddGraphTypes(ServiceLifetime.Scoped);


// ----> Configure <----
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
  app.UseDeveloperExceptionPage();

//app.MapControllers();

app.UseGraphQL<ISchema>();

app.UseGraphQLPlayground(new PlaygroundOptions()); // http://localhost:5000/ui/playground

app.SeedData().Run();