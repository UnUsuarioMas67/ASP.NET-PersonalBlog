using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalBlog.Services.Accounts;

namespace PersonalBlog.Filters;

public class EnsureLoggedIn : ActionFilterAttribute
{
    private readonly IAccountsService _accountsService;

    public EnsureLoggedIn()
    {
    }

    public EnsureLoggedIn(IAccountsService accountsService)
    {
        _accountsService = accountsService;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"] as string;
        if (action == "Login") return;

        if (!_accountsService.IsLoggedIn)
        {
            context.Result = new RedirectToActionResult("Login", "Admin", null);
        }

        base.OnActionExecuting(context);
    }
}