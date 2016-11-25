using System;
using System.Collections.Generic;
using Yonazone.Models;
using Yonazone.Data;

namespace Yonazone.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel 
    {
       public Product Product { get; set; }
        public ProductDetailViewModel () { }

        public ProductDetailViewModel(YonazoneContext ctx) : base(ctx) { }
    }
}
