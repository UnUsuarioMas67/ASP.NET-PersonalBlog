using PersonalBlog.Models;

namespace PersonalBlog.Services.Accounts;

public interface IAccountsService
{
    public Account? LoggedInAccount { get; }
    public bool IsLoggedIn { get; }
    void Login(string username, string password);
}