using DAL.Data;
using E_Cenima.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Cenima.Controllers
{
    public class AccountController(
        UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager,
        RoleManager<IdentityRole> _roleManager) : Controller
    {
        private readonly UserManager<ApplicationUser> userManager = _userManager;
        private readonly SignInManager<ApplicationUser> signInManager = _signInManager;
        private readonly RoleManager<IdentityRole> roleManager = _roleManager;

        // === USER REGISTER ===
        [HttpGet]
        public IActionResult RegisterUser() => View("RegisterUser");

        [HttpPost]
        public IActionResult RegisterUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                FullName = $"{model.FirstName} {model.LastName}",
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = userManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

            // Ensure 'User' role exists
            if (!roleManager.RoleExistsAsync(UserRole.User).Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole(UserRole.User)).Result;
                if (!roleResult.Succeeded)
                {
                    ModelState.AddModelError("", "Could not create User role.");
                    return View(model);
                }
            }

            userManager.AddToRoleAsync(user, UserRole.User).Wait();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult RegisterAdmin() => View();

        [HttpPost]
        public IActionResult RegisterAdmin(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                FullName = $"{model.FirstName} {model.LastName}",
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = userManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

            // Ensure 'Admin' role exists
            if (!roleManager.RoleExistsAsync(UserRole.Admin).Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole(UserRole.Admin)).Result;
                if (!roleResult.Succeeded)
                {
                    ModelState.AddModelError("", "Could not create Admin role.");
                    return View(model);
                }
            }

            userManager.AddToRoleAsync(user, UserRole.Admin).Wait();

            return RedirectToAction(nameof(Login));
        }

     
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                ModelState.AddModelError("", "Invalid password.");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login failed.");
                return View(model);
            }

            var roles = await userManager.GetRolesAsync(user);

         
            if (roles.Contains(UserRole.Admin))
                return RedirectToAction("AdminIndex", "Movie");

            return RedirectToAction("Index", "Home");
        }

      
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword() => View();
        public IActionResult ResetPassword() => View();
    }
}
