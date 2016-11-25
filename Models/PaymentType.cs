using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Yonazone.Models
{
  public class PaymentType
  {
    [Key]
    public int PaymentTypeId {get;set;}

    [Required]
    [StringLength(55)]
    public string Name { get; set; }

    [Required]
    [Display(Name= "Account Number", Description= "Please enter an account number")]
    [Range(10000000, 99999999)]
    public int AccountNumber {get;set;}

    [Required]
    [Range(100, 999)]
    public int CVV { get; set; }

    [Required]
    public DateTime Expiration { get; set; }

    [Required]
    public int UserId { get; set; }

    public User User {get;set;}
    
  }
}
