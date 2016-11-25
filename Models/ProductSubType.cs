using System.ComponentModel.DataAnnotations;

namespace Yonazone.Models
{
  public class ProductSubType
  {
    [Key]
    public int ProductSubTypeId {get;set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public int ProductTypeId { get; set; }
    public ProductType ProductType {get;set;}
  }
}