using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers;

public class LoginController : Controller
{
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var passwordHasher = new PasswordHasher<string>();
        var hashedPassword = passwordHasher.HashPassword(null, "admin");
        // hardcoded account for testing purposes
        var user = new User{ Username = "admin", Password = hashedPassword };

        if (model.User.Username == user.Username)
        {
            var result = passwordHasher.VerifyHashedPassword(null, user.Password, model.User.Password);
            if (result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.User.Username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Admin");
            }
        }
        
        model.Message = "Invalid username or password";
        return View(model);
    }
}