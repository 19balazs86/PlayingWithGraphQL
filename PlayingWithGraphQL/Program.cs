using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PlayingWithGraphQL.DataBase;

namespace PlayingWithGraphQL
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().SeedData().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
      => WebHost
          .CreateDefaultBuilder(args)
          .UseStartup<Startup>();
  }

  public static class WebHostExtensions
  {
    public static IWebHost SeedData(this IWebHost host)
    {
      using (IServiceScope scope = host.Services.CreateScope())
      {
        DataBaseContext dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

        dbContext.SeedData();
      }

      return host;
    }
  }
}
