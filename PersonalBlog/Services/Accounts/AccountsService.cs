using PersonalBlog.Models;

namespace PersonalBlog.Services.Accounts;

public class AccountsService : IAccountsService
{
    private readonly List<Account> _accounts = new List<Account>
    {
        new  Account() {Username = "unusuariomas67",  Password = "123456"},
        new  Account() {Username = "julioperez123",  Password = "123456"},
    };
    
    public Account? LoggedInAccount { get; private set; }
    public bool IsLoggedIn => LoggedInAccount != null;

    public bool Login(string username, string password)
    {
        LoggedInAccount = _accounts.SingleOrDefault(x => x.Username == username && x.Password == password);
        return IsLoggedIn;
    }
}