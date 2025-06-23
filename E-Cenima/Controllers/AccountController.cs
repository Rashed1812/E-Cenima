using DAL.Data;
using E_Cenima.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Cenima.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager ,SignInManager<ApplicationUser> _signInManager) : Controller
    {
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);
            var user = new ApplicationUser
            {
                FullName = registerViewModel.FirstName + " " + registerViewModel.LastName,
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
            };
            var result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
            if (result.Succeeded) return RedirectToAction("Login");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerViewModel);
            }
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);


            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user is not null)
            {
                var flag = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError(string.Empty, "Invalid Password");
            }
            else
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginViewModel);
        }
        public new  async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
