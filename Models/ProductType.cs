using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yonazone.Models
{
  public class ProductType
  {
      [Key]
      public int ProductTypeId {get;set;}

      [Required]
      [StringLength(55)]
      public string Name {get;set;}

      [Required]
      [StringLength(255)]
      public string Description {get;set;}
      [NotMappedAttribute]
      public int Quantity { get; set; }
      public ICollection<Product> Products {get;set;}

      public ICollection<ProductSubType> ProductSubType {get;set;} 
  }
}
