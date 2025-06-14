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

        if (IsUserValid(model.User))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.User.Username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Admin");
        }

        model.Message = "Invalid username or password";
        return View(model);
    }

    private bool IsUserValid(User user)
    {
        // Password for all users is: password
        
        var users = _configuration.GetSection("SiteUsers").Get<List<User>>();
        var passwordHasher = new PasswordHasher<string>();
        return users!.Any(u =>
        {
            var usernameMatches = user.Username == u.Username;
            var passwordMatches =
                passwordHasher.VerifyHashedPassword("", u.Password, user.Password)
                    is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
            return usernameMatches && passwordMatches;
        });
    }
}