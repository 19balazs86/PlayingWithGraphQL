using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayingWithGraphQL.Models
{
  public enum Gender { Man, Women }

  public class User
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public string Company { get; set; }
    public string Email { get; set; }
    public DateTime Registered { get; set; }
    public ICollection<Order> Orders { get; set; }
  }
}
