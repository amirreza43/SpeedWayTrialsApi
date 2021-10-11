using System;
using System.ComponentModel.DataAnnotations;

namespace web
{
  public class DriverDto
  {
    [Required]
    [MinLength(3)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(3)]
    public string LastName { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public string Nickname {set; get;}
    public int Wins {set; get;}
    public int Losses {set; get;}
  }
}
