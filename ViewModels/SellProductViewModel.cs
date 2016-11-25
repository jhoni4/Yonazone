using System.Collections.Generic;
using System.Linq;
using Yonazone.Models;
using Yonazone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yonazone.ViewModels
{
  public class SellProductViewModel : BaseViewModel
  {
    public Product Product {get;set;}

    public List<SelectListItem> ProductTypeId {get;set;}
    public SellProductViewModel(YonazoneContext ctx) : base(ctx) { }
  }
}