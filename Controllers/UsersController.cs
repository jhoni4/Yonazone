using Yonazone.Models;
using Microsoft.AspNetCore.Mvc;
using Yonazone.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Yonazone.ViewModels;

namespace Yonazone.Controllers
{
    public class UsersController : Controller
    {
        private YonazoneContext context;

        public UsersController(YonazoneContext ctx){
            context = ctx;
        }

        public IActionResult Create(){

            CreateUserViewModel model = new CreateUserViewModel(context);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.User.Add(user);
            try
            {
                context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            catch (DbUpdateException)
            {
                    throw;
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> users = from user in context.User select user;
            
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        public IActionResult Activate([FromRoute]int id)
        {
          var user = context.User.SingleOrDefault(c => c.UserId == id);

          if (user == null)
          {
            return NotFound();
          }

          ActiveUser.Instance.User = user;

          return Json(user);
        }
    }
}