using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yonazone.Models;
using Yonazone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yonazone.ViewModels;

namespace Yonazone.Controllers
{

/**
 * Class: OrdersController
 * Purpose: The OrdersController searches the database and returns all products in the customer's current order 
 * Author: Team Delta
 * Methods:
 *   OrdersController(database context) - constructor function that sets the database context
 *       variable - the Banagzon database context
 *   Index(integer variable) - this method traverses the database starting with the Order table, then adds the User table via the userId        foreign key, the OrderProducts via the userId foreign key, and the Products table via the OrderId foreign key. The method will return all of the user's products associated with the orderId, and the userId passed in as an argument. The method will only return orders with a paymentType of null indicating that this is the current order for the customer, not an order that has already been processed for this customer. 
 *       variable - integer userId
 */
    public class OrdersController : Controller
    {
        private YonazoneContext context;
        public OrdersController(YonazoneContext ctx)
        {
            context = ctx;
        }
        public async Task<IActionResult> Index([FromRoute]int id)
        {

            if (ActiveUser.Instance.User == null)
            {
                return RedirectToAction("Create", "Users");
            }

            var productsOnOrder =
                from ord in context.Order
                join uid in context.User on ord.UserId equals uid.UserId
                join op in context.OrderProduct on ord.OrderId equals op.OrderId
                join prod in context.Product on op.ProductId equals prod.ProductId
                where ord.UserId == ActiveUser.Instance.User.UserId 
                && ord.PaymentTypeId == null
                select prod;

            
            

            var userPaymentTypes = context.PaymentType
                .Where(p => p.UserId == ActiveUser.Instance.User.UserId)
                .AsEnumerable()
                .Select(li => new SelectListItem {
                    Text = li.Name.ToString(),
                    Value = li.PaymentTypeId.ToString()
                }).ToList();

            UserOrderViewModel uorder = new UserOrderViewModel(context);
            uorder.PaymentTypes = userPaymentTypes;
            uorder.Product = productsOnOrder
                             .OrderBy(l => l.Name);
                            
            double totalPrice = 0;
            var productsList = productsOnOrder.ToList();
            productsList.ForEach(p => totalPrice += p.Price);
            uorder.TotalOrderPrice = totalPrice;

            if (productsOnOrder == null)
            {
                // This is where we could build a new ViewModel for when the user's cart is empty
                // All of the logic for an empty order view will be within this IF block
                // In pseudo-code,, it might be something like...

                // EmptyOrderViewModel emptyOrder = new EmptyOrderViewModel();
                // return View(emptyOrder);

                return View();
            }
            return View(uorder);
        }

        public async Task<IActionResult> Create([FromRoute]int id)
        {
        // Query the database to see if there are any current orders with a PaymentType 
        // associated with the current user

            if (ActiveUser.Instance.User == null)
            {
                return RedirectToAction("Create", "Users");
            }

            Order currentOrder;

            var queriedOrder =
            from ord in context.Order
                join uid in context.User on ord.UserId equals uid.UserId
                where ord.UserId == ActiveUser.Instance.User.UserId 
                && ord.PaymentTypeId == null
                select ord;

            var emptyChecker = queriedOrder.SingleOrDefault();

        // if NO
        // create a new instance of Order() and give it the current user's UserId
        // and set PaymentType to null

            if (emptyChecker == null)
            {
                currentOrder = new Order();
                currentOrder.UserId = ActiveUser.Instance.User.UserId;
                currentOrder.PaymentTypeId = null;
                context.Add(currentOrder);
                await context.SaveChangesAsync();
            } else 
            {
                currentOrder = queriedOrder.SingleOrDefault();
            }
        // if YES
        // The order already exists, so do the following:
        // Create a new instance of OrderProduct() and give it the queried order's OrderId
        // AND the ProductId passed in from the route
            OrderProduct currentProduct = new OrderProduct();
            currentProduct.ProductId = id;
            currentProduct.OrderId = currentOrder.OrderId;
            context.Add(currentProduct);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Orders");
        }

        public async Task<IActionResult> Process([FromRoute] int? id)
        {
            if(id == null){
                return RedirectToAction("Index");
            }

            Order findOrder = context.Order
                    .SingleOrDefault(o => o.UserId == ActiveUser.Instance.User.UserId && o.PaymentTypeId == null);

            findOrder.PaymentTypeId = id;

            context.SaveChanges();

            return RedirectToAction("Confirmation", "Payment", new{id = findOrder.OrderId});
        }
    }
}
