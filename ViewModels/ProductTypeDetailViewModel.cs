using Yonazone.Models;
using Yonazone.Data;
using System.Collections.Generic;

namespace Yonazone.ViewModels
{
  public class ProductTypeDetailViewModel : BaseViewModel
  {
       public List<ProductSubType> ProductSubTypes { get; set; }
        public ProductSubType ProductSubType { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public ProductType ProductType { get; set; }
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public ProductTypeDetailViewModel () { }
    public ProductTypeDetailViewModel(YonazoneContext ctx) : base(ctx) { }
    }
}