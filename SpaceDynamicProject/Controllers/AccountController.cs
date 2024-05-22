using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SpaceDynamicProject.Enums;
using SpaceDynamicProject.Models;
using SpaceDynamicProject.ViewModels.Account;

namespace SpaceDynamicProject.Controllers
{
    public class AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {

            if(!ModelState.IsValid) return View(vm);

            AppUser user = new AppUser
            {
                Name = vm.Name,
                UserName = vm.Usename,
                Email = vm.Email,
                Surname = vm.Surname,
            };

            await _userManager.CreateAsync(user,vm.Password);

            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());

            return RedirectToAction("Login","Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {

            if (!ModelState.IsValid) return View(vm);

            AppUser user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);

            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
                if(user == null)
                {
                    ModelState.AddModelError("", "Username or password is false.");
                }
            }

            if (!ModelState.IsValid) return View(vm);

            await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);

            if (!result.Succeeded) 
            {
                ModelState.AddModelError("", "Someone is wrong");
            }

            if (!ModelState.IsValid) return View(vm);

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                if(!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }


            return Content("OK");
        }
    }
}
