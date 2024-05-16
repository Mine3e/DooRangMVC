using Business.Helpers.Account;
using Core.Models;
using Doorang.DTOs.AccountDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Doorang.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _rolemanager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Login(LoginDto loginDto, string ? returnUrl)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "UsernameOrEmail ve ya password dogru deyil");
                    return View();
                }
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden cehd edin");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya password duzdun deyil");
                return View();
            }
            await _signInManager.SignInAsync(user, loginDto.IsRemembered);
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user = new User()
            {
                UserName = registerDto.Usurname,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
            return RedirectToAction(nameof(Login));
        }
       
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async  Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                await _rolemanager.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString(),
                });
            }
            return Ok();
        }
    }
}
