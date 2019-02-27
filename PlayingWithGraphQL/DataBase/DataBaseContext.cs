using Microsoft.EntityFrameworkCore;
using PlayingWithGraphQL.Models;

namespace PlayingWithGraphQL.DataBase
{
  public class DataBaseContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {

    }
  }
}
