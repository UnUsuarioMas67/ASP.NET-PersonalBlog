using PersonalBlog.Models;

namespace PersonalBlog.Services.Accounts;

public interface IAccountsService
{
    Account? LoggedInAccount { get; }
    bool IsLoggedIn { get; }
    bool Login(string username, string password);
}