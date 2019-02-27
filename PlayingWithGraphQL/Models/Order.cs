using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayingWithGraphQL.Models
{
  public class Order
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }
    public User User { get; set; }
  }
}
