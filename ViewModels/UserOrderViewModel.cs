using System.Collections.Generic;
using System.Linq;
using Yonazone.Models;
using Yonazone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yonazone.ViewModels
{
    public class UserOrderViewModel : BaseViewModel 
    {
       public IEnumerable<Product> Product { get; set; }
        public List<SelectListItem> PaymentTypes {get;set;}
        public Order Order { get; set; }
        public Product product { get; set; }
        public User User { get; set; }
        public double TotalOrderPrice { get; set; }
        public UserOrderViewModel () { }
        public UserOrderViewModel(YonazoneContext ctx) : base(ctx) { }
    }
}


