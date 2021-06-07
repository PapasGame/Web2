using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Web2.Controllers
{
    [Authorize()]
    public class UserController : Controller
    {
        private DotsDBContext _context;
        public UserController(DotsDBContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Profile()
        {
            ProfileModel Model = new ProfileModel();
            Model.AllMap = _context.Maps.ToList();
            User AuthUser =  _context.Users.FirstOrDefault(u => u.Login == User.Identity.Name.ToString());
            Model.SubsMap = _context.UsersMaps.Include(u => u.Map).Where(u => u.UserId == AuthUser.UserId).ToList();
            
            return View(Model);
        } 
        
        public IActionResult SubscribeMap(int id)
        {
            User AuthUser = _context.Users.FirstOrDefault(u => u.Login == User.Identity.Name.ToString());
            UsersMap um = new UsersMap();
            um.MapId = id;
            um.UserId = AuthUser.UserId;
            um.User = AuthUser;
            um.Progress = "[{ }]";
            um.Map = _context.Maps.FirstOrDefault(u => u.MapId == id);


            UsersMap umCheck = _context.UsersMaps.Where(u => u.MapId == id && u.UserId == AuthUser.UserId).FirstOrDefault();
            if (umCheck==null)
            {
                _context.UsersMaps.Add(um);
                _context.SaveChanges();
            }
            
            return RedirectToRoute("default", new { controller = "Map", action = "WorkWithMap", id = id });
        }

        
    }
}
