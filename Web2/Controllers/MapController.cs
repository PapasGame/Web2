using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2.Controllers
{
    public class MapController : Controller
    {
        private DotsDBContext _context;
        public MapController(DotsDBContext context)
        {
            _context = context;

        }
        public IActionResult CreateMap()
        {
            //string str =" [\"Название\",{\"x\": -10,\"y\": -13,\"id\": \"1\",\"connections\": [2,3,4],\"label\": \"1\"}," +
            //    "{\"x\": -30,\"y\": 101,\"id\": \"2\",\"connections\": [3],\"label\": \"2\"},{\"x\": -164,\"y\": 2,\"id\": \"3\",\"connections\": [],\"label\": \"3\"},{\"x\": 128,\"y\": -73,\"id\": \"4\",\"connections\": [],\"label\": \"4\"}]";
            List<string> list = new List<string>();
            list.Add("1");
            list.Add("2");
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMap(Map map)
        {
            

            _context.Maps.Add(map);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public IActionResult WorkWithMap()
        {
            UsersMap um = _context.UsersMaps.Include(u => u.Map).FirstOrDefault();
            return View(um);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMap (int id, UsersMap map)
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
                    return WorkWithMap();
                }
            }
            return RedirectToAction(nameof(WorkWithMap));
        }
    }
}
