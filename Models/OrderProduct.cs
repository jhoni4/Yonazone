using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Yonazone.Models
{
  public class OrderProduct
  {
      [Key]
      public int OrderProductId {get;set;}

      [Required]
      public int ProductId {get; set;}

      public Product Product {get;set;}

      [Required]
      public int OrderId {get; set;}

      public Order Order {get;set;}
  }
}
