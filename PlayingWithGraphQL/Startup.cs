using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayingWithGraphQL.DataBase;
using PlayingWithGraphQL.GraphQL;

namespace PlayingWithGraphQL
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      //services.AddControllers();

      // --> EF: Use in-memory database.
      services.AddDbContext<DataBaseContext>(options => options.UseInMemoryDatabase("dbName"));

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
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

      //app.UseRouting();

      // --> GraphQL
      app.UseGraphQL<DefinitionSchema>(); // Default path: /graphql
      app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); // http://localhost:5000/ui/playground

      //app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
