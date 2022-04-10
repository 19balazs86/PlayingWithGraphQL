using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayingWithGraphQL.DataBase;

namespace PlayingWithGraphQL
{
  public class Program_OLD
  {
    public static void Main_OLD(string[] args)
    {
      CreateHostBuilder(args).Build().SeedData().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webHostBuilder =>
          webHostBuilder
            .UseStartup<Startup>()
            .ConfigureKestrel(options => options.AllowSynchronousIO = true));
            // TODO: Check. Because of the GraphQL. Their plan is to use the System.Text.Json to allow it async.
    }
  }

  public static class WebHostExtensions
  {
    public static IHost SeedData(this IHost host)
    {
      using IServiceScope scope = host.Services.CreateScope();

      DataBaseContext dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

      dbContext.SeedData();

      return host;
    }
  }
}
