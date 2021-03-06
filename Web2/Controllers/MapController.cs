using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2.Controllers
{
    //[Authorize()]
    public class MapController : Controller
    {
        private DotsDBContext _context;
        public MapController(DotsDBContext context)
        {
            _context = context;

        }
        public IActionResult CreateMap()
        {
            if (User.Identity.IsAuthenticated)
            {  
            return View();
            }
            return
                RedirectToRoute("default", new { controller = "Account", action = "Login", i = 2 }); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> AddMap(Map map)
        {
            

            _context.Maps.Add(map);
            await _context.SaveChangesAsync();

            return RedirectToRoute("default", new { controller = "User", action = "Profile"});
        }



        public IActionResult WorkWithMap(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                    UsersMap um;
                if (id == null)
                {
                    User AuthUser = _context.Users.FirstOrDefault(u => u.Login == User.Identity.Name.ToString());
                    um = _context.UsersMaps.Include(u => u.Map).Where(u => u.UserId == AuthUser.UserId).FirstOrDefault();
                }
                else
                {
                    User AuthUser = _context.Users.FirstOrDefault(u => u.Login == User.Identity.Name.ToString());

                    um = _context.UsersMaps.Include(u => u.Map).Where(u => u.MapId == id && u.UserId == AuthUser.UserId).FirstOrDefault();
                }
                if (um!=null)
                {
                    return View(um);
                }
                else
                {
                    return  RedirectToAction("Profile", "User");
                }
            }
            return
                RedirectToRoute("default", new { controller = "Account", action = "Login", i = 4 });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> EditMap (int? id, UsersMap map)
        {
             if (ModelState.IsValid)
            {
                UsersMap mapCh = await _context.UsersMaps.FirstOrDefaultAsync(u => u.UsersMapId == map.UsersMapId);
                if (mapCh != null)
                {
                    try
                    {
                        mapCh.Progress = map.Progress;
                        _context.Update(mapCh);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        UsersMap mapCheck = await _context.UsersMaps.FirstOrDefaultAsync(u => u.UsersMapId == map.UsersMapId);
                        if (mapCheck == null)

                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    return RedirectToAction("Profile", "User");
                }
            }
            return RedirectToAction("Profile", "User"); 
        }
    }
}
