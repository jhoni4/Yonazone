using System;
using System.Collections.Generic;
using Yonazone.Models;
using Yonazone.Data;

namespace Yonazone.ViewModels
{
    public class ProductTypeViewModel : BaseViewModel 
    {
       public List<Product> Products {get;set;}

        public ProductTypeViewModel(YonazoneContext ctx) : base(ctx) { }
    }
}
