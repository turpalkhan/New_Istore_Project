using Delete.ClassLibrary1.Models;
using Delete.ClassLibrary1;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Delete.Controllers
{
    public class Autification : Controller
    {
        private readonly ILogger<Autification> _logger;
        private readonly MainContext _mainContext;
        public int UserId;

        public Autification(ILogger<Autification> logger, MainContext mainContext)
        {
            _logger = logger;
            _mainContext = mainContext;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrModels models)
        {
            User user = await _mainContext.Users.FirstOrDefaultAsync(u => u.Name == models.Name);
            if (user == null)
            {   
                user = new User
                {
                    Name = models.Name,
                    Surname = models.Surname,
                    Login = models.Login,
                    Password = models.Password,
                    Email = models.Email,   
                };
                user.Role = Role.Пользователь.ToString();

                _mainContext.Users.Add(user);
                await _mainContext.SaveChangesAsync();
                Content(User.Identity.Name);


                await Authenticate(user); // аутентификация

                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Такой аккаунт уже существует!");

            return View();
        }
            
        [HttpGet]   
        public IActionResult Vhod()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vhod(VhodModels models)
        {
            User user = await _mainContext.Users.FirstOrDefaultAsync(u => u.Login == models.Login && u.Password == models.Password);
            if (user != null)
            {
                await Authenticate(user); // аутентификация
                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Ошибка в логине или пароль");

            return View(models);
        }

        public async Task<IActionResult> Account()
        {
            _mainContext.Users.UpdateRange();
            var UserAccount = _mainContext.Users.Where(x => x.Name == User.Identity.Name).ToList();
            return View(UserAccount);
        }


        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
            };
            UserId = user.Id;

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
