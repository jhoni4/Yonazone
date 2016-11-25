using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yonazone.Models;
using Yonazone.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yonazone.ViewModels
{
  public class PaymentTypeFormViewModel : BaseViewModel
  {

      public PaymentType PaymentType { get; set; }
      public PaymentTypeFormViewModel() {}
      public PaymentTypeFormViewModel(YonazoneContext ctx) : base(ctx) { }
      
  }
}