using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Yonazone.Models
{
  public class Order
  {
    [Key]
    public int OrderId {get;set;}

    public int UserId {get;set;}

    public User User {get;set;}

    //int? means that PaymentTypeId can contain an integer or be null...//
    public int? PaymentTypeId { get; set; }

    public PaymentType PaymentType {get;set;}

    public ICollection<OrderProduct> OrderProducts {get;set;}
  }
}
