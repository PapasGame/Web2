using Web2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web2.Controllers
{
    public class AccountController : Controller
    {
        private DotsDBContext _context;
        public AccountController(DotsDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new User
                    {
                        Login = model.Login,
                        Password = model.Password,
                        Email = model.Email
                    };

                    Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == 1);
                    if (userRole != null)
                    {
                        user.Role = userRole;
                        user.RoleId = userRole.RoleId;
                    }
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Не прошел валидацию");
            return View(model);
        }
       
        [HttpGet]
        public IActionResult Login(int? i)
        {
            LoginModel LM = new LoginModel();
            if (i == null)
            {
                LM.CheckNum = 1;
            }
            else
            {
                LM.CheckNum = (int)i;
            }
            return View(LM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация
                    if (model.CheckNum ==1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    if (model.CheckNum == 2)
                    {
                        return RedirectToAction("CreateMap", "Map");
                    }
                    if (model.CheckNum == 3)
                    {
                        return RedirectToAction("Profile", "User");
                    }
                    if (model.CheckNum == 4)
                    {
                        return RedirectToAction("WorkWithMap", "Map");
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
