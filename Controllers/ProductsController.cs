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
    public class ProductsController : Controller
    {
        private YonazoneContext context;
        public ProductsController(YonazoneContext ctx)
        {
            context = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var model = new ProductTypeViewModel(context);
            model.Products = await context.Product.ToListAsync();
            
            return View(model);
        }

        public async Task<IActionResult> ProductTypes()
        {
            var model = new ProductTypeDetailViewModel(context);
            model.ProductTypes = await context.ProductType.ToListAsync();

            var queried = 
            from prst in context.ProductSubType
            join prdt in context.ProductType on prst.ProductSubTypeId equals prdt.ProductTypeId
            where prst.ProductSubTypeId == prdt.ProductTypeId
            select prst;
 
            model.ProductSubTypes = await queried.ToListAsync();
 
           return View(model);
        }

        public async Task<IActionResult> ProductTypeDetail([FromRoute]int id)
        {
            // If no id was in the route, return 404
            if (id == null)
            {
                return NotFound();
            }
            var model = new ProductTypeDetailViewModel(context);
            model.ProductSubTypes = await context.ProductSubType.ToListAsync();

            var queriedProds = 
            from prd in context.Product
            join prst in context.ProductSubType on prd.ProductId equals prst.ProductSubTypeId
            where prd.ProductSubTypeId == id
            select prd;

            model.Products = await queriedProds.ToListAsync();

            return View(model);
        }
        public async Task<IActionResult> Detail([FromRoute]int? id)
        {
            // If no id was in the route, return 404
            if (id == null)
            {
                return NotFound();
            }
            var product = await context.Product
                    .Include(s => s.User)
                    .SingleOrDefaultAsync(m => m.ProductId == id);

            // If product not found, return 404
            if (product == null)
            {
                return NotFound();
            }

            ProductDetailViewModel model = new ProductDetailViewModel(context);
            model.Product = product;

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            if (ActiveUser.Instance.User == null)
            {
                return RedirectToAction("Create", "Users");
            }

            var ProductTypesId = context.ProductType
                                       .OrderBy(l => l.Name)
                                       .AsEnumerable()
                                       .Select(li => new SelectListItem { 
                                           Text = li.Name,
                                           Value = li.ProductTypeId.ToString()
                                        }).ToList();

            ProductTypesId.Insert(0, new SelectListItem{
                Text = "Select a product type!",
                Value = 0.ToString()
            });

            SellProductViewModel model = new SellProductViewModel(context);

            model.ProductTypeId = ProductTypesId;
                                        
            return View(model);
        }
        [HttpGet]
        public DbSet<ProductSubType> GetSubTypesForDropdown()
        {
            var subTypes  = context.ProductSubType;
            return subTypes; 
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            product.UserId = ActiveUser.Instance.User.UserId;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", "Products");
            }

            context.Add(product);

            try
            {
                context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            
            catch (DbUpdateException)
            {
                return RedirectToAction("Create", "Products");
            }
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetSubTypesForDropdown([FromBody] int id)
        {
            //this method is to create the dropdown for the user when they are creating a product and need to select a subtype
            //passing in the producttype id to the method
            IQueryable<object> subtypes = 
            from types in context.ProductType 
            // types = querying the database for all ProductTypes
            join subtype in context.ProductSubType on id equals subtype.ProductSubTypeId
            //then, go to the ProductSubType table and get all of those too
            where types.ProductTypeId == id 
            //then, get the subtypes that match the producttypeId we passed in
            select subtype;
            //BINGO!!^

            // return View(subtypes);
            ViewData["SubTypes"] = subtypes;
            return View();
            //this is the variable that we assigned our query to, and what we were searching the database for
        }

    }
}