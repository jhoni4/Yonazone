using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yonazone.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yonazone.Models;
using Yonazone.ViewModels;

namespace Yonazone.Controllers
{
    public class PaymentController : Controller
    {
        private YonazoneContext context;

        public PaymentController(YonazoneContext ctx)
        {
            context = ctx;
        }
        
        [HttpGet]
        public IActionResult AddPayment()

        {
            PaymentTypeFormViewModel model = new PaymentTypeFormViewModel(context);
            // var productsOnOrder =
            //     from ord in context.Order
            //     join uid in context.User on ord.UserId equals uid.UserId
            //     join op in context.OrderProduct on ord.OrderId equals op.OrderId
            //     join prod in context.Product on op.ProductId equals prod.ProductId
            //     where ord.UserId == ActiveUser.Instance.User.UserId 
            //     && ord.PaymentTypeId == null
            //     select prod;

            // model.count = productsOnOrder.Count();

            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentType paymenttype)
        {
            paymenttype.UserId = ActiveUser.Instance.User.UserId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.PaymentType.Add(paymenttype);

            try
            {
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Orders", ActiveUser.Instance.User.UserId);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
        
        public IActionResult Confirmation(int id)
        {
            ConfirmationViewModel model = new ConfirmationViewModel(context);
            model.OrderId = id;
            return View(model);
        }
    }
}



