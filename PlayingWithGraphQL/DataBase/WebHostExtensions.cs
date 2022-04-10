namespace PlayingWithGraphQL.DataBase
{
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
