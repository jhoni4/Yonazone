using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Yonazone.Models
{
  public class User
  {
    [Key]
    public int UserId {get;set;}

    [Required]
    [StringLength(55)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(55)]
    public string LastName { get; set; }

    public ICollection<Product> Products {get;set;}
  }
}
