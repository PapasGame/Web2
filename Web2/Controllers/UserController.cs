using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2.Controllers
{
    public class UserController : Controller
    {
        private DotsDBContext _context;
        public UserController(DotsDBContext context)
        {
            _context = context;

        }
        public IActionResult Profile()
        {
            

            return View();
        }
    }
}
