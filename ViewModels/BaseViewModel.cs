using System;
using System.Collections.Generic;
using System.Linq;
using Yonazone.Models;
using Yonazone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yonazone.ViewModels
{
  public class BaseViewModel
  {
    public List<SelectListItem> UserId { get; set; }
    private YonazoneContext context;
    // public int count { get; set; }
    private ActiveUser singleton = ActiveUser.Instance;
    public User ChosenUser 
    {
      get
      {
        // Get the current value of the customer property of our singleton
        User user = singleton.User;

        // If no customer has been chosen yet, it's value will be null
        if (user == null)
        {
          // Return fake customer for now
          return new User () {
            FirstName = "Create",
            LastName = "Account"
          };
        }

        // If there is a customer chosen, return it
        return user;
      }
      set
      {
        if (value != null)
        {
          singleton.User = value;
        }
      }
    }
     public int count()
     {
       int Count; 
       if(ActiveUser.Instance.User == null)
       {
         Count = 0;
         return Count;
       }
       var productsOnOrder =
                from ord in context.Order
                join uid in context.User on ord.UserId equals uid.UserId
                join op in context.OrderProduct on ord.OrderId equals op.OrderId
                join prod in context.Product on op.ProductId equals prod.ProductId
                where ord.UserId == ActiveUser.Instance.User.UserId 
                && ord.PaymentTypeId == null
                select prod;

       Count = productsOnOrder.Count();
       return Count;
     }
    public BaseViewModel(YonazoneContext ctx)
    {
        context = ctx;

        this.UserId = context.User
            .OrderBy(l => l.LastName)
            .AsEnumerable()
            .Select(li => new SelectListItem { 
                Text = $"{li.FirstName} {li.LastName}",
                Value = li.UserId.ToString()
            }).ToList();

        this.UserId.Insert(0, new SelectListItem { 
                Text = "Choose customer...",
                Value = "0"
            }); 
    }
    public BaseViewModel() { } 
  }
}